using ApiModels.Customers.Responses;

namespace WebClients.Abstractions;

public interface ICustomersWebClient
{
    Task<GetPaymentsHistoryResponse> GetPaymentsHistoryAsync(int customerId);
    Task<GetClothesResponse> GetClothesAsync(int customerId);
    Task<GetCustomerImageResponse> GetCustomerImageAsync(int customerId);
    Task<GetCustomerResponse> GetCustomerAsync(int customerId);
    Task<GetCustomersResponse> GetCustomersAsync();
}