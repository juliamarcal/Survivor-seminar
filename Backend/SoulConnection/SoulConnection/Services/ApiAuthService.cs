using Domain.Abstractions;
using Domain.Configurations;
using WebClients.Abstractions;

namespace SoulConnection.Services;

// todo: perhaps this class should be responsible for keeping access token active in cache and refreshing it
public class ApiAuthService : IApiAuthService
{
    private readonly ApiAccessConfiguration _configuration;
    private readonly IAuthWebClient _authWebClient;

    public ApiAuthService(ApiAccessConfiguration configuration, IAuthWebClient authWebClient)
    {
        _configuration = configuration;
        _authWebClient = authWebClient;
    }

    public async Task<string> ReceiveAuthTokenAsync()
    {
        var accessTokenResponse = await _authWebClient.LoginAsync();
        
        return accessTokenResponse.AccessToken;
    }

    public Task<string> ReceiveAccessTokenAsync()
    {
        return Task.FromResult(_configuration.ApiToken);
    }
}