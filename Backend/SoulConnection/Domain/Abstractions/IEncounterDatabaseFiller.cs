using ApiModels.Encounters;

namespace Domain.Abstractions;

public interface IEncounterDatabaseFiller
{
    Task FillDatabaseAsync(IList<DetailedEncounter> encounters);
}