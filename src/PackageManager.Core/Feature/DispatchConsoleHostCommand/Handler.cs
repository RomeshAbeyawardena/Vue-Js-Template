using MediatR;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Queries.DispatchConsoleHostCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Core.Feature.DispatchConsoleHostCommand
{
    public class Handler : IRequestHandler<Query>
    {
        private readonly IConsoleHostDispatcher consoleHostDispatcher;
        private readonly IMediator mediator;

        public Handler(IConsoleHostDispatcher consoleHostDispatcher,
            IMediator mediator)
        {
            this.consoleHostDispatcher = consoleHostDispatcher;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var consoleHost = await mediator.Send(new Shared.Queries.GetConsoleHost.Query { 
                Key = request.Key },
                cancellationToken);

            await consoleHostDispatcher.Dispatch(consoleHost, request.Arguments, cancellationToken);

            return Unit.Value;
        }
    }
}
