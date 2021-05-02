using MediatR;
using Microsoft.Extensions.Logging;
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
        protected ModuleBase(ILogger logger,
            IConfiguration configuration,
            IMediator mediator)
        {
            Configuration = configuration;
            Mediator = mediator;
            Logger = logger;
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

        protected ILogger Logger { get; }
        public IConfiguration Configuration { get; }
        public IMediator Mediator { get; }
    }
}
