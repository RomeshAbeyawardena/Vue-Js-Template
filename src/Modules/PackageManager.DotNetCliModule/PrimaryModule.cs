using Microsoft.Extensions.Logging;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Base;
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
        private readonly ILogger<PrimaryModule> logger;

        public PrimaryModule(IConfiguration configuration,
            ILogger<PrimaryModule> logger)
            : base(configuration)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public override Task<bool> CleanUpAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> RunAsync(CancellationToken cancellationToken)
        {
            var projectAddCommand = configuration
                .Commands.FirstOrDefault(a => a.Key == "Project.Add");

            var solutionAddCommand = configuration
                .Commands.FirstOrDefault(a => a.Key == "Solution.Add");

            var solutionDirectory = $"{configuration.Output}\\{configuration.SolutionName}";
            foreach (var project in configuration.ProjectNames)
            {
                var projectDirectory = $"{solutionDirectory}\\{configuration.SolutionName}.{project}";
                Console.Write("\r\nEnter project type for {0}: ", project);
                var type = Console.ReadLine();
                Process.Start("powershell", projectAddCommand.Value
                    .Replace("{project.type}", type)
                    .Replace("{solution.path}", projectDirectory))
                    .WaitForExit();

            }

            throw new NotImplementedException();
        }
    }
}
