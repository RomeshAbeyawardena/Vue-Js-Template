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
using DispatchConsoleHostCommandQuery = PackageManager.Shared.Queries.DispatchConsoleHostCommand.Query;
using GetConfigurationFilePathsQuery = PackageManager.Shared.Queries.GetConfigurationFilePaths.Query;
using GetFilesQuery = PackageManager.Shared.Queries.GetFiles.Query;
using CopyFileRequest = PackageManager.Shared.Queries.CopyFile.Request;
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

            var solutionDirectory = $"{configuration.Output}\\{configuration.SolutionName}";

            //Create new SLN file in solution directory
            await Mediator.Send(new DispatchConsoleHostCommandQuery
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

                //Create project of specified type
                await Mediator.Send(new DispatchConsoleHostCommandQuery
                {
                    Arguments = projectAddCommand.Value
                        .Replace(projectTypeParameter, type)
                        .Replace(solutionPathParameter, projectDirectory)
                }, cancellationToken);

                //Add project to solution
                await Mediator.Send(new DispatchConsoleHostCommandQuery
                {
                    Arguments = solutionAddProjectCommand.Value
                        .Replace(projectPathParameter, projectPath)
                        .Replace(solutionPathParameter, $"{solutionDirectory}\\{configuration.SolutionName}.sln")
                }, cancellationToken);

                if (configureWebApplicationWithRazorandVue)
                {
                    //Copy startup.cs from template directory
                    var startupFilePath = $"{projectDirectory}\\Startup.cs";
                    
                    await Mediator.Send(new CopyFileRequest { 
                        SourcePath = "Templates/Web/Startup.cs.txt",
                        DestinationPath = startupFilePath,
                        OverWriteFile = true
                    });

                    var text = System.IO.File.ReadAllText(startupFilePath);
                    //replace placeholder in copied filed
                    System.IO.File.WriteAllText(startupFilePath, text.Replace("{project.name}", projectName));


                    //get outputs from configuration
                    var filePaths = await Mediator.Send(
                        new GetConfigurationFilePathsQuery { 
                            Name = "Web" }, 
                        cancellationToken);

                    foreach (var filePath in filePaths)
                    {
                        var files = await Mediator
                            .Send(new GetFilesQuery { 
                                FilePath = filePath.Source, 
                                ExtensionDelimiter = ',', 
                                Extensions = filePath.FileExtensions }, 
                                cancellationToken);

                        foreach (var file in files.Files)
                        {
                            var relativeFilePath = filePath.To.Concat(file.FullName.Replace(files.Directory.FullName, string.Empty));

                            await Mediator.Send(new CopyFileRequest { 
                                CreateSubDirectories = true,
                                SourcePath = file.FullName, 
                                DestinationPath = $"{solutionDirectory}\\{relativeFilePath}", 
                                OverWriteFile = true }, cancellationToken);
                        }
                    }

                    //webOutputs.FileExtensions.Select(a => a.Value);
                }
            }

            return true;
        }
    }
}
