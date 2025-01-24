using ApiModels.Events;

namespace Domain.Abstractions;

public interface IEventDatabaseFiller
{
    Task FillDatabaseAsync(IList<DetailedEvent> events);
}