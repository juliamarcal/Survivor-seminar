using ApiModels.Encounters;
using Domain.Abstractions;
using LinqToDB;
using LinqToDB.Data;
using Repository.Entities;

namespace SoulConnection.Services;

public class EncounterDatabaseFiller(DataConnection dataConnection) : IEncounterDatabaseFiller
{
    public async Task FillDatabaseAsync(IList<DetailedEncounter> encounters)
    {
        var entities = encounters.Select(x => new EncounterEntity()
            {
                Id = x.Id,
                Date = DateTime.TryParse(x.Date, out var date) ? date : default,
                Comment = x.Comment,
                Rating = x.Rating,
                Source = x.Source,
                CustomerId = x.CustomerId,
            })
            .ToList();

        var existingEntities = entities
            .Where(x => dataConnection
                .GetTable<EncounterEntity>()
                .Any(y => y.Id == x.Id))
            .ToList();

        var newEntities = entities
            .Except(existingEntities)
            .ToList();

        await dataConnection.GetTable<EncounterEntity>().BulkCopyAsync(newEntities);

        // todo: modify existing events in table
    }
}