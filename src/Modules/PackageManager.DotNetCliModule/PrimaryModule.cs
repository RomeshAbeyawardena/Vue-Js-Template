using MediatR;
using Microsoft.Extensions.Logging;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Base;
using PackageManager.Shared.Domain.Models;
using PackageManager.Shared.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GetConfigurationCommandQuery = PackageManager.Shared.Queries.GetConfigurationCommand.Query;

namespace PackageManager.DotNetCliModule
{
    public class PrimaryModule : ModuleBase
    {
        private readonly IConfiguration configuration;
        
        private Task<Command> GetCommandByKey(string key)
        {
            return Mediator
               .Send(new GetConfigurationCommandQuery { 
                   Key = key 
               });
        }

        public PrimaryModule(IConfiguration configuration,
            ILogger<PrimaryModule> logger,
            IMediator mediator)
            : base(logger, configuration, mediator)
        {
            this.configuration = configuration;
        }

        public override Task<bool> CleanUpAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> RunAsync(CancellationToken cancellationToken)
        {
            const string projectTypeParameter = "{project.type}";
            const string solutionPathParameter = "{solution.path}";
            const string projectPathParameter = "{project.path}";

            var solutionAddProjectCommand = await GetCommandByKey("Solution.Add");

            var projectAddCommand = await GetCommandByKey("Project.Add");

            var solutionAddCommand = await GetCommandByKey("Solution.Add");

            var solutionDirectory = $"{configuration.Output}\\{configuration.SolutionName}";

            await Mediator.Send(new Shared.Queries.DispatchConsoleHostCommand.Query
            {
                Arguments = projectAddCommand.Value
                .Replace(projectTypeParameter, "sln")
                .Replace(solutionPathParameter, solutionDirectory)
            }, cancellationToken);

            bool configureWebApplicationWithRazorandVue = false;
            foreach (var project in configuration.ProjectNames)
            {
                var projectName = $"{configuration.SolutionName}.{project}";
                var projectPath = $"{solutionDirectory}\\{projectName}\\";
                var projectDirectory = $"{solutionDirectory}\\{projectName}";
                Console.Write("\r\nEnter project type for {0}: ", projectName);
                var type = Console.ReadLine();

                if (type.Equals("web", StringComparison.InvariantCultureIgnoreCase) 
                    && projectName.EndsWith("Web", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Write("Configure this web application with an empty razor + vue template? Y|Yes or N|No");
                    var response = Console.ReadLine();
                    configureWebApplicationWithRazorandVue = response.Equals("Yes", 
                                StringComparison.InvariantCultureIgnoreCase) 
                            || response.Equals("Y", 
                                StringComparison.InvariantCultureIgnoreCase);
                }

                await Mediator.Send(new Shared.Queries.DispatchConsoleHostCommand.Query
                {
                    Arguments = projectAddCommand.Value
                        .Replace(projectTypeParameter, type)
                        .Replace(solutionPathParameter, projectDirectory)
                }, cancellationToken);

                await Mediator.Send(new Shared.Queries.DispatchConsoleHostCommand.Query
                {
                    Arguments = solutionAddProjectCommand.Value
                        .Replace(projectPathParameter, projectPath)
                        .Replace(solutionPathParameter, $"{solutionDirectory}\\{configuration.SolutionName}.sln")
                }, cancellationToken);

                if (configureWebApplicationWithRazorandVue)
                {
                    var startupFilePath = $"{projectDirectory}\\Startup.cs";
                    System.IO.File.Copy("Templates/Web/Startup.cs.txt", startupFilePath, true);
                    var text = System.IO.File.ReadAllText(startupFilePath);

                    System.IO.File.WriteAllText(startupFilePath, text.Replace("{project.name}", projectName));

                    var webOutputs = configuration.Outputs.First(o => o.Name == "Web");
                    var di = new System.IO.DirectoryInfo("Templates/Web/App");
                    di.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                    //webOutputs.FileExtensions.Select(a => a.Value);
                }
            }

            return true;
        }
    }
}
