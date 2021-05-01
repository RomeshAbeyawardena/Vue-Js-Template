using Microsoft.Extensions.Logging;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Base;
using PackageManager.Shared.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.DotNetCliModule
{
    public class PrimaryModule : ModuleBase
    {
        private readonly IConfiguration configuration;
        private readonly IConsoleHostDispatcher consoleHostDispatcher;
        private readonly ILogger<PrimaryModule> logger;

        public PrimaryModule(IConfiguration configuration,
            IConsoleHostDispatcher consoleHostDispatcher,
            ILogger<PrimaryModule> logger)
            : base(configuration)
        {
            this.configuration = configuration;
            this.consoleHostDispatcher = consoleHostDispatcher;
            this.logger = logger;
        }

        public override Task<bool> CleanUpAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> RunAsync(CancellationToken cancellationToken)
        {
            
            const string projectTypeParameter = "{project.type}";
            var projectAddCommand = configuration
                .Commands.First(a => a.Key == "Project.Add");

            var solutionAddCommand = configuration
                .Commands.First(a => a.Key == "Solution.Add");

            var consoleHost = consoleHostDispatcher.DefaultConsoleHost;

            var solutionDirectory = $"{configuration.Output}\\{configuration.SolutionName}";

            await consoleHostDispatcher.Dispatch(consoleHost, projectAddCommand
                .Value
                .Replace(projectTypeParameter, "sln")
                .Replace("{solution.path}", solutionDirectory));

            foreach (var project in configuration.ProjectNames)
            {
                var projectName = $"{configuration.SolutionName}.{project}";
                var projectDirectory = $"{solutionDirectory}\\{projectName}";
                Console.Write("\r\nEnter project type for {0}: ", projectName);
                var type = Console.ReadLine();

                await consoleHostDispatcher.Dispatch(consoleHost, projectAddCommand.Value
                        .Replace($"{projectTypeParameter}", type)
                        .Replace("{solution.path}", projectDirectory));
            }

            throw new NotImplementedException();
        }
    }
}
