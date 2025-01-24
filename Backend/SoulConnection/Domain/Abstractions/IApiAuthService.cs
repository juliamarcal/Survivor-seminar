namespace Domain.Abstractions;

public interface IApiAuthService
{
    /// <summary>
    /// Returns bearer auth token.
    /// </summary>
    /// <returns></returns>
    public Task<string> ReceiveAuthTokenAsync();
    
    /// <summary>
    /// Returns API access token.
    /// </summary>
    /// <returns></returns>
    public Task<string> ReceiveAccessTokenAsync();
}