using LinqToDB.Mapping;

namespace Repository.Entities;

[Table("Encounter")]
public class EncounterEntity
{
    [PrimaryKey] [Column("UniqueID")] public int Id { get; set; }
    [Column("customer_id")] public int CustomerId { get; set; }
    [Column("date")] public DateTime Date { get; set; }
    [Column("rating")] public int Rating { get; set; }
    [Column("comment")] public string Comment { get; set; }
    [Column("source")] public string Source { get; set; }
}