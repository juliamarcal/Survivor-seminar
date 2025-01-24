using ApiModels.Employees;
using Domain.Abstractions;
using Domain.Abstractions.Migrators;
using Domain.Configurations;
using WebClients.Abstractions;

namespace SoulConnection.Migrators;

public class EmployeeMigrator(
    ILogger<EmployeeMigrator> logger,
    MigrationConfiguration configuration,
    IEmployeeWebClient webClient,
    IEmployeeDatabaseFiller databaseFiller
    ): IEmployeeMigrator
{
    public async Task MigrateAsync()
    {
        logger.LogInformation("Employees data migration began. Fetching all employees.");

        var response = await webClient.GetEmployeesAsync();
        var employees = response.Employees.ToList();
        var offset = 0;

        while (offset < employees.Count)
        {
            logger.LogDebug("Fetching employee extra data.");
            
            var batchTasks = employees
                .Skip(offset)
                .Take(configuration.DataBatchSize)
                .Select(x => webClient.GetEmployeeAsync(x.Id))
                .ToArray();

            var responses = await Task.WhenAll(batchTasks);
            var detailedEvents = responses
                .Select(DetailedEmployee (x) => x)
                .ToList();

            logger.LogDebug("Updating database.");

            await databaseFiller.FillDatabaseAsync(detailedEvents);
            
            logger.LogDebug("Employees batch has been inserted into database.");

            offset += configuration.DataBatchSize;

            await Task.Delay(configuration.DelayAfterBatch.Milliseconds);
        }
        
        logger.LogInformation("Employees data has been migrated.");
    }
}