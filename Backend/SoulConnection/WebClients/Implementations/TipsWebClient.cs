using ApiModels.Tips;
using ApiModels.Tips.Responses;
using Domain;
using Domain.Exceptions;
using Newtonsoft.Json;
using WebClients.Abstractions;

namespace WebClients.Implementations;

public sealed class TipsWebClient(Uri baseAddress, IHttpClientPool httpClientPool)
    : WebClientBase(baseAddress), ITipsWebClient
{
    private const string Endpoint = "/api/tips";

    public async Task<GetTipsResponse> GetTipsAsync()
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(baseAddress, Endpoint);
        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
        
        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<List<TipDto>>(jsonString);

            return new GetTipsResponse(deserialized);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }
}