using System.Net.Mime;
using System.Text;
using ApiModels.Employees.Requests;
using ApiModels.Employees.Responses;
using Domain;
using Domain.Configurations;
using Domain.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebClients.Abstractions;

namespace WebClients.Implementations;

public sealed class AuthWebClient : IAuthWebClient
{
    private readonly ApiAccessConfiguration _configuration;
    private readonly Uri _baseUri;

    public AuthWebClient(ApiAccessConfiguration configuration, Uri baseUri)
    {
        _configuration = configuration;
        _baseUri = baseUri;
    }

    public async Task<AccessTokenResponse> LoginAsync()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Group-Authorization", _configuration.ApiToken);
        
        var requestUri = new Uri(_baseUri, "/api/employees/login");
        var requestContent = new LoginRequest(_configuration.AuthEmail, _configuration.AuthSecret);
        
        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        var serializedContent = JsonConvert.SerializeObject(requestContent, serializerSettings);
        
        using var jsonContent = new StringContent(serializedContent, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await client.PostAsync(requestUri, jsonContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }

        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<AccessTokenResponse>(jsonString);

            return deserialized;
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }
}