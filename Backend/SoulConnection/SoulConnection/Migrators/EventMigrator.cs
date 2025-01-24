using ApiModels.Events;
using Domain.Abstractions;
using Domain.Abstractions.Migrators;
using Domain.Configurations;
using WebClients.Abstractions;

namespace SoulConnection.Migrators;

public class EventMigrator(
    ILogger<EventMigrator> logger,
    MigrationConfiguration configuration,
    IEventsWebClient webClient,
    IEventDatabaseFiller databaseFiller)
    : IEventMigrator
{
    public async Task MigrateAsync()
    {
        logger.LogInformation("Events data migration began. Fetching all events.");

        var response = await webClient.GetEventsAsync();
        var events = response.Events;
        var offset = 0;

        while (offset < events.Count)
        {
            logger.LogDebug("Fetching event extra data.");
            
            var batchTasks = events
                .Skip(offset)
                .Take(configuration.DataBatchSize)
                .Select(x => webClient.GetEventAsync(x.Id))
                .ToArray();

            var responses = await Task.WhenAll(batchTasks);
            var detailedEvents = responses
                .Select(DetailedEvent (x) => x)
                .ToList();

            logger.LogDebug("Updating database.");

            await databaseFiller.FillDatabaseAsync(detailedEvents);
            
            logger.LogDebug("Events batch has been inserted into database.");

            offset += configuration.DataBatchSize;
            
            await Task.Delay(configuration.DelayAfterBatch.Milliseconds);
        }

        logger.LogInformation("Events data has been migrated.");
    }
}