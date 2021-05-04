using MediatR;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Queries.GetConsoleHost;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Core.Feature.GetConsoleHost
{
    public class Handler : IRequestHandler<Query, IConsoleHost>
    {
        private readonly IConfiguration configuration;
        private readonly IConsoleHostDispatcher consoleHostDispatcher;

        public Handler(IConfiguration configuration,
            IConsoleHostDispatcher consoleHostDispatcher)
        {
            this.configuration = configuration;
            this.consoleHostDispatcher = consoleHostDispatcher;
        }

        public Task<IConsoleHost> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(string.IsNullOrEmpty(request.Key)
                ? consoleHostDispatcher.DefaultConsoleHost
                : configuration.ConsoleHosts.First(a => a.Key.Equals(request.Key,
                    StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
