using MediatR;
using Microsoft.Extensions.Logging;
using PackageManager.Shared.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Shared.Base
{
    public abstract class ModuleBase : IModule
    {
        protected ModuleBase(ILogger logger,
            ISystemConsole systemConsole,
            IConfiguration configuration,
            IMediator mediator)
        {
            Configuration = configuration;
            Mediator = mediator;
            Logger = logger;
            Console = systemConsole;
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

        public ISystemConsole Console { get; }
        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }
        public IMediator Mediator { get; }
    }
}
