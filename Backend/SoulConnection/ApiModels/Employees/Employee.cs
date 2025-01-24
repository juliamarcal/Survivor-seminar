using ApiModels.Common;

namespace ApiModels.Employees;

public record Employee(int Id, string Email, string Name, string Surname) : Person(Id, Email, Name, Surname);