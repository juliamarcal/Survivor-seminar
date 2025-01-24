namespace Domain.Abstractions.Migrators;

public interface IEmployeeMigrator
{
    Task MigrateAsync();
}