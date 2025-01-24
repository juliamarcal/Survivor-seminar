using ApiModels.Customers;
using Domain.Abstractions;
using WebClients.Abstractions;

namespace SoulConnection.Services;

public class CustomerDataCollector : ICustomerDataCollector
{
    private readonly ICustomersWebClient _customerWebClient;
    private readonly IEncountersWebClient _encountersWebClient;

    public CustomerDataCollector(ICustomersWebClient customerWebClient, IEncountersWebClient encountersWebClient)
    {
        _customerWebClient = customerWebClient;
        _encountersWebClient = encountersWebClient;
    }

    public async Task<IList<DetailedCustomer>> CollectDetailedCustomersAsync(IList<Customer> customers)
    {
        var result = new List<DetailedCustomer>(customers.Count);

        foreach (var customer in customers)
        {
            DetailedCustomer detailedCustomer = await _customerWebClient.GetCustomerAsync(customer.Id);
            result.Add(detailedCustomer);
        }

        return result;
    }

    public async Task<IList<DateTime>> CollectRegistrationDatesAsync(IList<Customer> customers)
    {
        var result = new List<DateTime>(customers.Count);

        foreach (var customer in customers)
        {
            var response = await _encountersWebClient.GetEncountersByCustomerAsync(customer.Id);

            if (response.Encounters.Count > 0)
            {
                var registrationDate = response.Encounters
                    .Select(encounter => encounter.Date)
                    .Select(x => DateTime.TryParse(x, out var date) ? date : default)
                    .OrderBy(date => date)
                    .First();
                
                result.Add(registrationDate);
            }
            else
            {
                result.Add(default);
            }
        }

        return result;
    }
}