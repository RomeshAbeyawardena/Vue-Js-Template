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
            const string consoleArgumentsParameter = "{console.arguments}";

            request.Parameters.Add(consoleArgumentsParameter, request.Arguments);

            var consoleHost = await mediator.Send(new Shared.Queries.GetConsoleHost.Query { 
                Key = request.Key },
                cancellationToken);

            foreach (var (key, value) in request.Parameters)
            {
                request.Arguments = request.Arguments.Replace(key, value);
                request.WorkingDirectory = request.WorkingDirectory.Replace(key, value);
            }

            await consoleHostDispatcher.Dispatch(consoleHost, request.Arguments, request.WorkingDirectory, cancellationToken);

            return Unit.Value;
        }
    }
}
