namespace WebClients.Implementations;

public abstract class WebClientBase(Uri baseAddress)
{
    /// <summary>
    /// Should contain protocol and host parts.
    /// </summary>
    protected Uri BaseAddress { get; init; } = baseAddress;
}