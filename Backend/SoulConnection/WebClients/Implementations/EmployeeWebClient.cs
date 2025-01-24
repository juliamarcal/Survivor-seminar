using System.Net.Mime;
using System.Text;
using ApiModels.Clothes.Responses;
using ApiModels.Employees;
using ApiModels.Employees.Requests;
using ApiModels.Employees.Responses;
using Domain;
using Domain.Configurations;
using Domain.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebClients.Abstractions;

namespace WebClients.Implementations;

public sealed class EmployeeWebClient(Uri baseAddress, ApiAccessConfiguration configuration, IHttpClientPool httpClientPool)
    : WebClientBase(baseAddress), IEmployeeWebClient
{
    private readonly IHttpClientPool _httpClientPool = httpClientPool;
    private const string Endpoint = "/api/employees";

    public async Task<GetEmployeesResponse> GetEmployeesAsync()
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
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<List<Employee>>(jsonString, serializerSettings);

            return new GetEmployeesResponse(deserialized);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }
    
    public async Task<GetEmployeeMeResponse> GetEmployeeMeAsync()
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(baseAddress, $"{Endpoint}/me");
        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
        
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
            var deserialized = JsonConvert.DeserializeObject<GetEmployeeMeResponse>(jsonString, serializerSettings);

            return deserialized;
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetEmployeeImageResponse> GetEmployeeImageAsync(int employeeId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(baseAddress, $"{Endpoint}/{employeeId}/image");
        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
        
        try
        {
            var inputStream = await response.Content.ReadAsByteArrayAsync();
            
            return new GetEmployeeImageResponse(inputStream);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetEmployeeResponse> GetEmployeeAsync(int employeeId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(baseAddress, $"{Endpoint}/{employeeId}");
        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
        
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
            var deserialized = JsonConvert.DeserializeObject<GetEmployeeResponse>(jsonString, serializerSettings);

            return deserialized;
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

}