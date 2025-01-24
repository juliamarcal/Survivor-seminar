using LinqToDB.Mapping;

namespace Repository.Entities;

[Table("Customer")]
public class CustomerEntity
{
    [PrimaryKey] [Column("UniqueID")] public int CustomerId { get; set; }
    [Column("name")] public string Name { get; set; }
    [Column("surname")] public string Surname { get; set; }
    [Column("email")] public string Email { get; set; }
    
    [Column("birth_date")] public DateTime BirthDate { get; set; }
    [Column("gender")] public string Gender { get; set; }
    [Column("description")] public string Description { get; set; }
    [Column("astrological_sign")] public string AstrologicalSign { get; set; }

    [Column("phone_number")] public string PhoneNumber { get; set; }
    [Column("address")] public string Address { get; set; }
    [Column("joined_on")] public DateTime JoinedOn { get; set; }
}