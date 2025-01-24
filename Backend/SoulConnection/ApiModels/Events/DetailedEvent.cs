namespace ApiModels.Events;

public record DetailedEvent(
    int Id,
    string Name,
    string Date,
    int MaxParticipants,
    string LocationX,
    string LocationY,
    string Type,
    int EmployeeId,
    string LocationName
) : Event(Id, Name, Date, MaxParticipants);