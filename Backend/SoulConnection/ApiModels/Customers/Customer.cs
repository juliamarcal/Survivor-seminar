using ApiModels.Common;

namespace ApiModels.Customers;

public record Customer(
    int Id,
    string Email,
    string Name,
    string Surname) : Person(Id, Email, Name, Surname);