using ApiModels.Clothes.Responses;
using Domain;
using Domain.Exceptions;
using WebClients.Abstractions;

namespace WebClients.Implementations;

public sealed class ClothesWebClient(Uri baseAddress, IHttpClientPool httpClientPool) : WebClientBase(baseAddress), IClothesWebClient
{
    private readonly IHttpClientPool _httpClientPool = httpClientPool;
    private const string Endpoint = "api/clothes";
    public async Task<GetClotheImageResponse> GetClotheImageAsync(int clotheId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, $"{Endpoint}/{clotheId}/image");
        var response = await client.GetAsync(requestUri);
        
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
        
        try
        {
            var inputStream = await response.Content.ReadAsByteArrayAsync();
            
            return new GetClotheImageResponse(inputStream);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }
    
}