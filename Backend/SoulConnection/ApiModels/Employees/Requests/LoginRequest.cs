using Newtonsoft.Json;

namespace ApiModels.Employees.Requests;

public record LoginRequest([JsonProperty("email")] string Email, [JsonProperty("password")] string Password);