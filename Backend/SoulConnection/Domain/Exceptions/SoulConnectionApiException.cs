using System.Net;

namespace Domain.Exceptions;

public class SoulConnectionApiException : SoulConnectionException
{
    public SoulConnectionApiException(HttpStatusCode statusCode) : base()
    {
        StatusCode = statusCode;
    }

    public SoulConnectionApiException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public SoulConnectionApiException(HttpStatusCode statusCode, string message, Exception innerException) : base(
        message, innerException)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }

}