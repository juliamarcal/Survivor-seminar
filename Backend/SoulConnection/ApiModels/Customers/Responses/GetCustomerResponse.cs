namespace ApiModels.Customers.Responses;

public record GetCustomerResponse(
    int Id,
    string Email,
    string Name,
    string Surname,
    string BirthDate,
    string Gender,
    string Description,
    string AstrologicalSign,
    string PhoneNumber,
    string Address
) : DetailedCustomer(Id, Email, Name, Surname, BirthDate, Gender, Description, AstrologicalSign, PhoneNumber, Address);