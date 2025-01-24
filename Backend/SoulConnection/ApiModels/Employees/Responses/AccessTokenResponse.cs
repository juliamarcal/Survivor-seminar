using Newtonsoft.Json;

namespace ApiModels.Employees.Responses;

/// <summary>
/// DTO containing token required for API requests.
/// </summary>
/// <param name="AccessToken">Bearer token itself.</param>
public record AccessTokenResponse([JsonProperty("access_token")] string AccessToken);