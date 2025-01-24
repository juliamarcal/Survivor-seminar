namespace ApiModels.Employees.Responses;

public record GetEmployeeMeResponse(
    int Id,
    string Email,
    string Name,
    string Surname,
    string BirthDate,
    string Gender,
    string Work) : DetailedEmployee(Id, Email, Name, Surname, BirthDate, Gender, Work);