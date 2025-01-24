using ApiModels.Employees;
using Domain.Abstractions;
using LinqToDB;
using LinqToDB.Data;
using Repository.Entities;

namespace SoulConnection.Services;

public class EmployeeDatabaseFiller(DataConnection dataConnection) : IEmployeeDatabaseFiller
{
    public async Task FillDatabaseAsync(IList<DetailedEmployee> employees)
    {
        var entities = employees.Select(x => new EmployeeEntity()
            {
                EmployeeId = x.Id,
                BirthDate = DateTime.TryParse(x.BirthDate, out var date) ? date : default,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Gender = x.Gender,
                Work = x.Work
            })
            .ToList();
        
        var existingEntities = entities
            .Where(x => dataConnection
                .GetTable<EmployeeEntity>()
                .Any(y => y.EmployeeId == x.EmployeeId))
            .ToList();

        var newEntities = entities
            .Except(existingEntities)
            .ToList();

        await dataConnection.GetTable<EmployeeEntity>().BulkCopyAsync(newEntities);
        
        // todo: modify existing events in table
    }
}