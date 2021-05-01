using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
