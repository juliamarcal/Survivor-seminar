
namespace Domain.Exceptions;

public class SoulConnectionException : Exception
{
    public SoulConnectionException() : base() {}
    public SoulConnectionException(string message) : base(message) {}
    public SoulConnectionException(string message, Exception innerException) : base(message, innerException) {}

    public static SoulConnectionException JsonDeserializationFailure(Exception innerException)
    {
        return new SoulConnectionException("Failed to deserialize json.", innerException);
    }
}