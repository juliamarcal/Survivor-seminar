namespace ApiModels.Encounters;

public record DetailedEncounter(
    int Id,
    int CustomerId,
    string Date,
    int Rating,
    string Comment,
    string Source
) : Encounter(Id, CustomerId, Date, Rating);