using ApiModels.Encounters.Responses;

namespace WebClients.Abstractions;

public interface IEncountersWebClient
{
    Task<GetEncounterResponse> GetEncounterAsync(int encounterId);
    Task<GetEncountersResponse> GetEncountersAsync();
    Task<GetEncounterByCustomerResponse> GetEncountersByCustomerAsync(int customerId);
}