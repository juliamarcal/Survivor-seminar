using System.Net.Http.Headers;
using Domain.Abstractions;
using WebClients.Abstractions;

namespace WebClients.Implementations;

public class HttpClientPool : IHttpClientPool
{
    private readonly IApiAuthService _apiAuthService;

    public HttpClientPool(IApiAuthService apiAuthService)
    {
        _apiAuthService = apiAuthService;
    }

    public async Task<HttpClient> GetHttpClientAsync()
    {
        var client = new HttpClient();
        var accessToken = await _apiAuthService.ReceiveAccessTokenAsync();
        var authToken = await _apiAuthService.ReceiveAuthTokenAsync();
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        client.DefaultRequestHeaders.Add("X-Group-Authorization", accessToken);

        return client;
    }
}