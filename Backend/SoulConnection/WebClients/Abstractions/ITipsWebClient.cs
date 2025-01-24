using ApiModels.Tips.Responses;

namespace WebClients.Abstractions;

public interface ITipsWebClient
{
    Task<GetTipsResponse> GetTipsAsync();
}