using ApiModels.Clothes.Responses;

namespace WebClients.Abstractions;

public interface IClothesWebClient
{
    Task<GetClotheImageResponse> GetClotheImageAsync(int clotheId);
}