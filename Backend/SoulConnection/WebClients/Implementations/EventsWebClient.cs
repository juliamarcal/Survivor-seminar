using ApiModels.Events;
using ApiModels.Events.Responses;
using Domain;
using Domain.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebClients.Abstractions;

namespace WebClients.Implementations;

public sealed class EventsWebClient(Uri baseAddress, IHttpClientPool httpClientPool) : WebClientBase(baseAddress), IEventsWebClient
{
    private const string Endpoint = "/api/events";
    private readonly IHttpClientPool _httpClientPool = httpClientPool;
    
    public async Task<GetEventsResponse> GetEventsAsync()
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, Endpoint);
        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
        
        try
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<List<Event>>(jsonString, serializerSettings);
            
            return new GetEventsResponse(deserialized);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetEventResponse> GetEventAsync(int eventId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, BuildSpecificEventEndpoint(eventId));
        var response = await client.GetAsync(requestUri);
        
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
        
        try
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<GetEventResponse>(jsonString, serializerSettings);
            
            return deserialized;
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    private static string BuildSpecificEventEndpoint(int eventId)
    {
        return $"{Endpoint}/{eventId}";
    }
}