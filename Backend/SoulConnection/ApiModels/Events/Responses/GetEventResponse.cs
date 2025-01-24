namespace ApiModels.Events.Responses;

public record GetEventResponse(
    int Id,
    string Name,
    string Date,
    int MaxParticipants,
    string LocationX,
    string LocationY,
    string Type,
    int EmployeeId,
    string LocationName
) : DetailedEvent(Id, Name, Date, MaxParticipants, LocationX, LocationY, Type, EmployeeId, LocationName);