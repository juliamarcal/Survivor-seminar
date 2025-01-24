using ApiModels.Events.Responses;

namespace WebClients.Abstractions;

public interface IEventsWebClient
{
    Task<GetEventsResponse> GetEventsAsync();
    Task<GetEventResponse> GetEventAsync(int eventId);
}