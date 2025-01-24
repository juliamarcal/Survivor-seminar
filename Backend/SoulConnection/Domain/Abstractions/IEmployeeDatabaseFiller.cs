using ApiModels.Employees;

namespace Domain.Abstractions;

public interface IEmployeeDatabaseFiller
{
    Task FillDatabaseAsync(IList<DetailedEmployee> employees);
}