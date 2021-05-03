using MediatR;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Domain.Models;
using PackageManager.Shared.Queries.GetConfigurationCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Core.Feature.GetConfigurationCommand
{
    public class Handler : IRequestHandler<Query, Command>
    {
        private readonly IConfiguration configuration;

        public Handler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<Command> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task
                .FromResult(configuration.Commands.First(a => a.Key.Equals(request.Key, 
                    StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
