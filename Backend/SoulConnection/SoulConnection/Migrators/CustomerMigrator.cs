using Domain.Abstractions;
using Domain.Abstractions.Migrators;
using Domain.Configurations;
using WebClients.Abstractions;

namespace SoulConnection.Migrators;

public class CustomerMigrator(
    ILogger<CustomerMigrator> logger,
    MigrationConfiguration configuration,
    ICustomersWebClient webClient,
    ICustomerDatabaseFiller databaseFiller,
    ICustomerDataCollector dataCollector)
    : ICustomerMigrator
{
    public async Task MigrateAsync()
    {
        logger.LogInformation("Customers data migration began. Fetching all customers.");
        
        var response = await webClient.GetCustomersAsync();
        var customers = response.Customers;
        var offset = 0;

        while (offset < customers.Count)
        {
            var batch = customers
                .Skip(offset)
                .Take(configuration.DataBatchSize)
                .ToList();

            logger.LogDebug("Fetching customers extra data.");
            var detailedCustomers = await dataCollector.CollectDetailedCustomersAsync(batch);

            logger.LogDebug("Fetching customers registration dates.");
            var registrationDates = await dataCollector.CollectRegistrationDatesAsync(batch);

            logger.LogDebug("Updating database.");
            await databaseFiller.FillDatabaseAsync(detailedCustomers, registrationDates);
            
            logger.LogDebug("Customers batch has been inserted into database.");
            
            offset += configuration.DataBatchSize;
            
            await Task.Delay(configuration.DelayAfterBatch.Milliseconds);
        }

        logger.LogInformation("Customers data has been migrated.");
    }
}