namespace Domain.Abstractions.Migrators;

public interface ICustomerMigrator
{
    Task MigrateAsync();
}