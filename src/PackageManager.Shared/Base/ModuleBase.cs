using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Shared.Base
{
    public abstract class ModuleBase : IModule
    {
        public IConfiguration Configuration { get;  }

        protected ModuleBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public abstract Task<bool> RunAsync(CancellationToken cancellationToken);
        public abstract Task<bool> CleanUpAsync(CancellationToken cancellationToken);

        public virtual void Dispose()
        {
        }

        public virtual ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
