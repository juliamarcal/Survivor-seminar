using Newtonsoft.Json;

namespace ApiModels.Tips;

// public record TipDto([JsonProperty("id")] int Id, [JsonProperty("title")]string Title, [JsonProperty("tip")]string Tip);
public record TipDto(int Id, string Title,string Tip);
