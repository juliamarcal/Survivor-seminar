using Domain.Abstractions.Migrators;
using Microsoft.AspNetCore.Mvc;

namespace SoulConnection.Controllers;

[Controller]
[Route("api/migrate")]
public class MigrationController(
    ILogger<MigrationController> logger,
    ICustomerMigrator customerMigrator,
    IEventMigrator eventMigrator,
    IEmployeeMigrator employeeMigrator,
    IEncounterMigrator encounterMigrator)
    : Controller
{
    [HttpPost("customers")]
    public Task MigrateCustomersAsync()
    {
        logger.LogWarning("Manual customers data migration started.");

        return customerMigrator.MigrateAsync();
    }

    [HttpPost("events")]
    public Task MigrateEventsAsync()
    {
        logger.LogWarning("Manual events data migration started.");

        return eventMigrator.MigrateAsync();
    }
    
    [HttpPost("employees")]
    public Task MigrateEmployeesAsync()
    {
        logger.LogWarning("Manual employees data migration started.");

        return employeeMigrator.MigrateAsync();
    }
    
    [HttpPost("encounters")]
    public Task MigrateEncountersAsync()
    {
        logger.LogWarning("Manual encounters data migration started.");
        
        return encounterMigrator.MigrateAsync();
    }
}