using Microsoft.Extensions.Hosting;

namespace Domain.Abstractions;

public interface IDataSynchronizer : IHostedService
{
    Task SynchronizeAsync(CancellationToken cancellationToken);
}