using LinqToDB.Mapping;

namespace Repository.Entities;

[Table("Event")]
public class EventEntity
{
    [PrimaryKey] [Column("UniqueID")] public int Id { get; set; }
    [Column("name")] public string Name { get; set; }
    [Column("date")] public DateTime Date { get; set; }
    [Column("max_participants")] public int MaxParticipants { get; set; }
    [Column("location_x")] public float LocationX { get; set; }
    [Column("location_y")] public float LocationY { get; set; }
    [Column("type")] public string Type { get; set; }
    [Column("employee_id")] public int EmployeeId { get; set; }
    [Column("location_name")] public string LocationName { get; set; }
}