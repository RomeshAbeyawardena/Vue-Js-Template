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
using PackageManager.Shared;

namespace PackageManager.DotNetCliModule
{
    public class PrimaryModule : ModuleBase
    {
        private const string ProjectTypeParameter = "{project.type}";
        private const string SolutionPathParameter = "{solution.path}";
        private const string ProjectPathParameter = "{project.path}";
        private static string NewLine = Environment.NewLine;


        private readonly IConfiguration configuration;
        
        private Task<Command> GetCommandByKey(string key,
            CancellationToken cancellationToken)
        {
            return Mediator
               .Send(new GetConfigurationCommandQuery { 
                   Key = key 
               });
        }

        private Task CreateSolutionFile(Command projectAddCommand, 
            string solutionDirectory, 
            CancellationToken cancellationToken)
        {
            //Create new SLN file in solution directory
            return Mediator.Send(new DispatchConsoleHostCommandQuery(a => a
                .Add(ProjectTypeParameter, "sln")
                .Add(SolutionPathParameter, solutionDirectory)) {
                    Arguments = projectAddCommand.Value
                }, cancellationToken);
        }

        private Task CreateProject(string type,
            string projectDirectory,  Command projectAddCommand, 
            CancellationToken cancellationToken)
        {
            return Mediator.Send(new DispatchConsoleHostCommandQuery(a => a
                .Add(ProjectTypeParameter, type)
                .Add(SolutionPathParameter, projectDirectory)) {
                    Arguments = projectAddCommand.Value
                }, cancellationToken);
        }

        private Task AddProjectToSolution(Command solutionAddProjectCommand,
            string projectPath, string solutionDirectory, CancellationToken cancellationToken)
        {
            return Mediator.Send(new DispatchConsoleHostCommandQuery(a => a
                .Add(ProjectPathParameter, projectPath)
                .Add(SolutionPathParameter, 
                        $"{solutionDirectory}\\{configuration.SolutionName}.sln")) {
                    Arguments = solutionAddProjectCommand.Value
                }, cancellationToken);
        }

        private async Task CopyStartupToWebProject(
            string projectDirectory, string projectName,
            CancellationToken cancellationToken)
        {
            var startupFilePath = $"{projectDirectory}\\Startup.cs";

            await Mediator.Send(new CopyFileRequest
            {
                SourcePath = "Templates/Web/Startup.cs.txt",
                DestinationPath = startupFilePath,
                OverWriteFile = true
            }, cancellationToken);

            var text = System.IO.File.ReadAllText(startupFilePath);
            //replace placeholder in copied filed
            System.IO.File.WriteAllText(startupFilePath, text.Replace("{project.name}", projectName));
        }

        private async Task CopyContentFilesToWebProject(string projectPath, 
            CancellationToken cancellationToken)
        {
            var filePaths = await Mediator.Send(
                        new GetConfigurationFilePathsQuery
                        {
                            Name = "Web"
                        },
                        cancellationToken);

            foreach (var filePath in filePaths)
            {
                var files = await Mediator
                    .Send(new GetFilesQuery
                    {
                        FilePath = filePath.Source,
                        ExtensionDelimiter = ',',
                        Extensions = filePath.FileExtensions
                    },
                        cancellationToken);

                foreach (var file in files.Files)
                {
                    var relativeFilePath = filePath.To.Concat(file.FullName
                        .Replace(files.Directory.FullName, string.Empty));

                    await Mediator.Send(new CopyFileRequest
                    {
                        CreateSubDirectories = true,
                        SourcePath = file.FullName,
                        DestinationPath = $"{projectPath}\\{relativeFilePath}",
                        OverWriteFile = true
                    }, cancellationToken);
                }
            }
        }

        private bool ProcessUserPromptToCopyWebRazorAndVueContentFiles(string type, string projectName)
        {
            if (type.Equals("web", StringComparison.InvariantCultureIgnoreCase)
                    && projectName.EndsWith("Web", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Configure this web application with an empty razor + vue template? Y|Yes or N|No: ");
                var response = Console.ReadLine();
                return response.Equals("Yes",
                                StringComparison.InvariantCultureIgnoreCase)
                            || response.Equals("Y",
                                StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        private Task RunPackageManager(Command packageManagerCommand, 
            string projectPath,
            CancellationToken cancellationToken)
        {
            return Mediator.Send(new DispatchConsoleHostCommandQuery
            {
                WorkingDirectory = projectPath,
                Arguments = packageManagerCommand.Value
            }, cancellationToken);
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
            var solutionAddProjectCommand = await GetCommandByKey("Solution.Add", cancellationToken);

            var projectAddCommand = await GetCommandByKey("Project.Add", cancellationToken);

            var solutionDirectory = $"{configuration.Output}\\{configuration.SolutionName}";

            await CreateSolutionFile(projectAddCommand, solutionDirectory, cancellationToken);

            bool configureWebApplicationWithRazorandVue = false;
            foreach (var project in configuration.ProjectNames)
            {
                var projectName = $"{configuration.SolutionName}.{project}";
                var projectPath = $"{solutionDirectory}\\{projectName}\\";
                var projectDirectory = $"{solutionDirectory}\\{projectName}";
                Console.Write("{0}Enter project type for {1}: ", NewLine, projectName);
                var type = Console.ReadLine();

                configureWebApplicationWithRazorandVue = ProcessUserPromptToCopyWebRazorAndVueContentFiles(type, projectName);

                //Create project of specified type
                await CreateProject(type, projectDirectory, projectAddCommand, cancellationToken);

                //Add project to solution
                await AddProjectToSolution(solutionAddProjectCommand, projectPath, 
                    solutionDirectory, cancellationToken);

                if (configureWebApplicationWithRazorandVue)
                {
                    //Copy startup.cs from template directory
                    await CopyStartupToWebProject(projectDirectory, projectName, cancellationToken);

                    await CopyContentFilesToWebProject(projectPath, cancellationToken);

                    char userSelection = default;
                    var commandNameDictionaryBuilder = DictionaryBuilder.Create<char, string>()
                        .Add('1', "Npm.Install")
                        .Add('2', "Yarn.Install");
                    string commandName;
                    while (!commandNameDictionaryBuilder.TryGetValue(userSelection, out commandName))
                    {
                        if(userSelection != default)
                        {
                            Console.WriteLine($"{NewLine}Invalid selection.{NewLine}");
                        }

                        Console.WriteLine("Which package manager should be used for client files?");
                        Console.Write($"\t 1. NPM{NewLine}" +
                            $"\t2. Yarn{NewLine}{NewLine}Please select:\t");
                        userSelection = Console.ReadKey().KeyChar;
                    }

                    var packageManagerCommand = await GetCommandByKey(commandName, cancellationToken);

                    await RunPackageManager(packageManagerCommand, projectPath, cancellationToken);
                }
            }

            return true;
        }
    }
}
