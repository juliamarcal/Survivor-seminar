namespace ApiModels.Encounters.Responses;

public record GetEncounterResponse(
    int Id,
    int CustomerId,
    string Date,
    int Rating,
    string Comment,
    string Source
) : DetailedEncounter(Id, CustomerId, Date, Rating, Comment, Source);