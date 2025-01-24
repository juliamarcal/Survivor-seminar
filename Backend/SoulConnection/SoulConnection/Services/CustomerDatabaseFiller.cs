using ApiModels.Customers;
using Domain.Abstractions;
using LinqToDB;
using LinqToDB.Data;
using Repository.Entities;

namespace SoulConnection.Services;

public class CustomerDatabaseFiller(DataConnection dataConnection) : ICustomerDatabaseFiller
{
    public async Task FillDatabaseAsync(IList<DetailedCustomer> customers, IList<DateTime> registrationDates)
    {
        var entities = customers
            .Zip(registrationDates)
            .Select(x => new CustomerEntity()
            {
                CustomerId = x.First.Id,
                Name = x.First.Name,
                Surname = x.First.Surname,
                Email = x.First.Email,
                Gender = x.First.Gender,
                Description = x.First.Description,
                BirthDate = DateTime.TryParse(x.First.BirthDate, out var date) ? date : default,
                AstrologicalSign = x.First.AstrologicalSign,
                PhoneNumber = x.First.PhoneNumber,
                Address = x.First.Address,
                JoinedOn = x.Second,
            })
            .ToList();

        // await dataConnection.InsertOrReplaceAsync(entities); // doesn't work properly

        var existingEntities = entities
            .Where(x => dataConnection.GetTable<CustomerEntity>().Any(y => x.CustomerId == y.CustomerId))
            .ToList();

        var newEntities = entities
            .Except(existingEntities)
            .ToList();

        await dataConnection.GetTable<CustomerEntity>().BulkCopyAsync(newEntities);

        // todo: update existingEntities? optimize mapping?
    }
}