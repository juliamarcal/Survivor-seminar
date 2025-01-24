using ApiModels.Employees.Responses;

namespace WebClients.Abstractions;

public interface IAuthWebClient
{
    Task<AccessTokenResponse> LoginAsync();
}