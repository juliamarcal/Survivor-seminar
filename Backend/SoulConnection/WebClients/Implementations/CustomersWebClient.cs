using ApiModels.Customers;
using ApiModels.Customers.Responses;
using Domain;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebClients.Abstractions;

namespace WebClients.Implementations;

public sealed class CustomersWebClient(
    Uri baseAddress,
    IHttpClientPool httpClientPool,
    ILogger<CustomersWebClient> logger)
    : WebClientBase(baseAddress), ICustomersWebClient
{
    private readonly IHttpClientPool _httpClientPool = httpClientPool;
    private const string Endpoint = "api/customers";
    private readonly ILogger<CustomersWebClient> _logger = logger;

    public async Task<GetPaymentsHistoryResponse> GetPaymentsHistoryAsync(int customerId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, $"{Endpoint}/{customerId}/payments_history");
        var response = await client.GetAsync(requestUri);

        await CheckErrorCode(response);

        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<List<Payment>>(jsonString);

            return new GetPaymentsHistoryResponse(deserialized);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetClothesResponse> GetClothesAsync(int customerId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, $"{Endpoint}/{customerId}/clothes");
        var response = await client.GetAsync(requestUri);

        await CheckErrorCode(response);

        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<List<ClothesItem>>(jsonString);

            return new GetClothesResponse(deserialized);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetCustomerImageResponse> GetCustomerImageAsync(int customerId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, $"{Endpoint}/{customerId}/image");
        var response = await client.GetAsync(requestUri);

        await CheckErrorCode(response);

        try
        {
            var inputStream = await response.Content.ReadAsByteArrayAsync();

            return new GetCustomerImageResponse(inputStream);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetCustomerResponse> GetCustomerAsync(int customerId)
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, $"{Endpoint}/{customerId}");
        var response = await client.GetAsync(requestUri);

        await CheckErrorCode(response);

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
            var deserialized = JsonConvert.DeserializeObject<GetCustomerResponse>(jsonString, serializerSettings);

            return deserialized;
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    public async Task<GetCustomersResponse> GetCustomersAsync()
    {
        using var client = await httpClientPool.GetHttpClientAsync();
        var requestUri = new Uri(BaseAddress, Endpoint);
        var response = await client.GetAsync(requestUri);

        await CheckErrorCode(response);
        
        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<List<Customer>>(jsonString);

            return new GetCustomersResponse(deserialized);
        }
        catch (Exception e)
        {
            throw SoulConnectionException.JsonDeserializationFailure(e);
        }
    }

    private async Task CheckErrorCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            var content = await response.Content.ReadAsStringAsync();
            throw new SoulConnectionApiException(response.StatusCode, content);
        }
    }
}