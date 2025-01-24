using ApiModels.Customers;

namespace Domain.Abstractions;

public interface ICustomerDataCollector
{
    Task<IList<DetailedCustomer>> CollectDetailedCustomersAsync(IList<Customer> customers);
    Task<IList<DateTime>> CollectRegistrationDatesAsync(IList<Customer> customers);
}