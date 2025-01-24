using ApiModels.Common;

namespace ApiModels.Employees;

public record DetailedEmployee(
    int Id,
    string Email,
    string Name,
    string Surname,
    string BirthDate,
    string Gender,
    string Work
) : Person (Id, Email, Name, Surname);
