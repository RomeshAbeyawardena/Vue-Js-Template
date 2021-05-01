using SystemConsole = System.Console;
using Utility.CommandLine;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Threading;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Domain.Models;
using PackageManager.Shared;
using Microsoft.Extensions.Logging;

namespace PackageManager.Console
{
    class Program
    {
        [Argument('s', "solutionName", "Name of the solution")]
        static string SolutionName { get; set; }

        [Argument('o', "output", "Output of solution and projects")]
        static string Output { get; set; }

        [Argument('n', "projectNames", "Output of files")]
        static string ProjectNames { get; set; }

        [Argument('x', "XML configuration path", "Enables custom configuration")]
        static string XmlConfigurationPath { get; set; }

        static StartupHost startupHost;

        static async Task Main(string[] args)
        {
            SystemConsole.CancelKeyPress += SystemConsole_CancelKeyPress;
            Arguments.Populate();

            startupHost = new StartupHost(RegisterServices);

            await startupHost.StartAsync(cancellationTokenSource.Token);
        }

        private static void SystemConsole_CancelKeyPress(object sender, System.ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            cancellationTokenSource.Cancel();
            startupHost
                .StopAsync(cancellationTokenSource.Token)
                .Wait();
        }

        static IServiceCollection RegisterServices(IServiceCollection services)
        {
            var projectNamesList = ProjectNames.Split(',');

            return services
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddSingleton(serviceProvider => LoggerFactory.Create(a => a.AddConsole()))
                .AddSingleton<Shared.Abstractions.IConfiguration>(new Configuration(SolutionName, Output, 
                    projectNamesList, XmlConfigurationPath))
                .AddSingleton<IConfigurationLoader, ConfigurationLoader>()
                .AddSingleton<IModuleLoader, ModuleLoader>()
                .AddSingleton<IConsoleHostDispatcher, ConsoleHostDispatcher>();
        }

        static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    }
}
