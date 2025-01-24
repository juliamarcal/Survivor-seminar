using ApiModels.Common;
using Newtonsoft.Json;

namespace ApiModels.Customers;

public record DetailedCustomer(
    int Id,
    string Email,
    string Name,
    string Surname,
    [JsonProperty("birth_date")] string BirthDate,
    string Gender,
    string Description,
    [JsonProperty("astrological_sign")] string AstrologicalSign,
    [JsonProperty("phone_number")] string PhoneNumber,
    string Address
) : Person(Id, Email, Name, Surname);