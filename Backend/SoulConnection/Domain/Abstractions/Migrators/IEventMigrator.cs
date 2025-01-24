namespace Domain.Abstractions.Migrators;

public interface IEventMigrator
{
    Task MigrateAsync();
}