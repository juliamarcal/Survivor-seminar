using ApiModels.Encounters;
using Domain.Abstractions;
using Domain.Abstractions.Migrators;
using Domain.Configurations;
using WebClients.Abstractions;

namespace SoulConnection.Migrators;

public class EncounterMigrator(
    ILogger<EncounterMigrator> logger,
    MigrationConfiguration configuration,
    IEncountersWebClient webClient,
    IEncounterDatabaseFiller databaseFiller
    ) : IEncounterMigrator
{
    public async Task MigrateAsync()
    {
        logger.LogInformation("Encounters data migration began. Fetching all encounters.");

        var response = await webClient.GetEncountersAsync();
        var events = response.Encounters;
        var offset = 0;

        while (offset < events.Count)
        {
            logger.LogInformation("Fetching encounter extra data.");
            
            var batchTasks = events
                .Skip(offset)
                .Take(configuration.DataBatchSize)
                .Select(x => webClient.GetEncounterAsync(x.Id))
                .ToArray();

            var responses = await Task.WhenAll(batchTasks);
            var detailedEvents = responses
                .Select(DetailedEncounter (x) => x)
                .ToList();

            logger.LogDebug("Updating database.");

            await databaseFiller.FillDatabaseAsync(detailedEvents);
            
            logger.LogDebug("Encounters batch has been inserted into database.");

            offset += configuration.DataBatchSize;
            
            await Task.Delay(configuration.DelayAfterBatch.Milliseconds);
        }

        logger.LogInformation("Encounters data has been migrated.");
    }
}