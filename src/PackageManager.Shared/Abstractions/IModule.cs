using System;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Shared.Abstractions
{
    public interface IModule : IDisposable, IAsyncDisposable
    {
        IConfiguration Configuration { get; }
        Task<bool> RunAsync(CancellationToken cancellationToken);
        Task<bool> CleanUpAsync(CancellationToken cancellationToken);
    }
}
