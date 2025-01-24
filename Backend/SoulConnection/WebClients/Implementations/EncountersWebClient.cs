using ApiModels.Encounters;
using ApiModels.Encounters.Responses;
using Domain;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebClients.Abstractions;

namespace WebClients.Implementations;

public class EncountersWebClient(Uri baseAddress, IHttpClientPool httpClientPool, ILogger<EncountersWebClient> logger)
    : WebClientBase(baseAddress), IEncountersWebClient
{
    private readonly IHttpClientPool _httpClientPool = httpClientPool;
    private const string Endpoint = "api/encounters";
    private readonly ILogger<EncountersWebClient> _logger = logger;

    public async Task<GetEncounterResponse> GetEncounterAsync(int encounterId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, $"{Endpoint}/{encounterId}");
        var response = await client.GetAsync(requestUri);

        await CheckErrorCode(response);

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
            var deserialized = JsonConvert.DeserializeObject<GetEncounterResponse>(jsonString, serializerSettings);

            return deserialized;
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetEncountersResponse> GetEncountersAsync()
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, Endpoint);
        var response = await client.GetAsync(requestUri);

        await CheckErrorCode(response);

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
            var deserialized = JsonConvert.DeserializeObject<List<Encounter>>(jsonString, serializerSettings);

            return new GetEncountersResponse(deserialized);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetEncounterByCustomerResponse> GetEncountersByCustomerAsync(int customerId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, $"{Endpoint}/customer/{customerId}");
        var response = await client.GetAsync(requestUri);

        await CheckErrorCode(response);

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
            var deserialized = JsonConvert.DeserializeObject<List<Encounter>>(jsonString, serializerSettings);

            return new GetEncounterByCustomerResponse(deserialized);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }
    
    private async Task CheckErrorCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
    }
}