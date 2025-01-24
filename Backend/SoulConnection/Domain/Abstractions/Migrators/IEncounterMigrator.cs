namespace Domain.Abstractions.Migrators;

public interface IEncounterMigrator
{
    Task MigrateAsync();
}