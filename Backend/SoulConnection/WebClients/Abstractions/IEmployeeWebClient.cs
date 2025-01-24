using ApiModels.Employees.Responses;

namespace WebClients.Abstractions;

public interface IEmployeeWebClient
{
    Task<GetEmployeesResponse> GetEmployeesAsync();
    Task<GetEmployeeMeResponse?> GetEmployeeMeAsync();
    Task<GetEmployeeImageResponse> GetEmployeeImageAsync(int employeeId);
    Task<GetEmployeeResponse> GetEmployeeAsync(int employeeId);
}