using LinqToDB.Mapping;

namespace Repository.Entities;

[Table("Employee")]
public class EmployeeEntity
{
    [PrimaryKey] [Column("UniqueID")] public int EmployeeId { get; set; }
    [Column("name")] public string Name { get; set; }
    [Column("email")] public string Email { get; set; }
    [Column("surname")] public string Surname { get; set; }

    [Column("birth_date")] public DateTime BirthDate { get; set; }
    [Column("gender")] public string Gender { get; set; }
    [Column("work")] public string Work { get; set; }
}