using ApiModels.Customers;

namespace Domain.Abstractions;

public interface ICustomerDatabaseFiller
{
    Task FillDatabaseAsync(IList<DetailedCustomer> customers, IList<DateTime> registrationDates);
}