using ApiModels.Events;
using Domain.Abstractions;
using LinqToDB;
using LinqToDB.Data;
using Repository.Entities;

namespace SoulConnection.Services;

public class EventDatabaseFiller(DataConnection dataConnection) : IEventDatabaseFiller
{
    private readonly DataConnection _dataConnection = dataConnection;

    public async Task FillDatabaseAsync(IList<DetailedEvent> events)
    {
        var entities = events.Select(x => new EventEntity()
            {
                Id = x.Id,
                MaxParticipants = x.MaxParticipants,
                LocationX = float.TryParse(x.LocationX, out var locationX) ? locationX : default,
                LocationY = float.TryParse(x.LocationY, out var locationY) ? locationY : default,
                EmployeeId = x.EmployeeId,
                LocationName = x.LocationName,
                Type = x.Type,
                Date = DateTime.TryParse(x.Date, out var date) ? date : default,
                Name = x.Name,
            })
            .ToList();

        var existingEntities = entities
            .Where(x => dataConnection
                .GetTable<EventEntity>()
                .Any(y => y.Id == x.Id))
            .ToList();

        var newEntities = entities
            .Except(existingEntities)
            .ToList();

        await dataConnection.GetTable<EventEntity>().BulkCopyAsync(newEntities);
        
        // todo: modify existing events in table
    }
}