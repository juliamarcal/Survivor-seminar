using Domain.Abstractions;
using Domain.Abstractions.Migrators;
using Domain.Configurations;

namespace SoulConnection.Services;

public class DataSynchronizer(
    MigrationConfiguration migrationConfiguration,
    ILogger<DataSynchronizer> logger,
    ICustomerMigrator customerMigrator,
    IEventMigrator eventMigrator,
    IEmployeeMigrator employeeMigrator,
    IEncounterMigrator encounterMigrator)
    : IDataSynchronizer
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        SynchronizeAsync(cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task SynchronizeAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                // todo: saga pattern
                
                logger.LogInformation("Synchronizing...");
                
                var customerMigrationTask = customerMigrator.MigrateAsync();
                var eventMigrationTask = eventMigrator.MigrateAsync();
                var employeeMigrationTask = employeeMigrator.MigrateAsync();
                var encounterMigrationTask = encounterMigrator.MigrateAsync();

                await Task.WhenAll
                (
                    customerMigrationTask,
                    eventMigrationTask,
                    employeeMigrationTask,
                    encounterMigrationTask
                );
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to perform data synchronization.");
            }

            logger.LogInformation("Synchronization done.");
            
            var delayTime = TimeSpan.FromSeconds(migrationConfiguration.SynchronizationPeriod.Seconds);
            await Task.Delay(delayTime, cancellationToken);
        }
    }
}