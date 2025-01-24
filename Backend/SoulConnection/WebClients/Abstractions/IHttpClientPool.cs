namespace WebClients.Abstractions;

public interface IHttpClientPool
{
    Task<HttpClient> GetHttpClientAsync();
}