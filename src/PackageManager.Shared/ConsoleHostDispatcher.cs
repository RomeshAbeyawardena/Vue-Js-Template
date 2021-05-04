using Microsoft.Extensions.Logging;
using PackageManager.Shared.Abstractions;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Shared
{
    public class ConsoleHostDispatcher : IConsoleHostDispatcher
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<IConsoleHostDispatcher> logger;

        public ConsoleHostDispatcher(IConfiguration configuration,
            ILogger<IConsoleHostDispatcher> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public IConsoleHost DefaultConsoleHost => configuration.ConsoleHosts.Single(a => a.Default);

        public Task Dispatch(IConsoleHost consoleHost, string arguments,
            string workingDirectory = default, CancellationToken cancellationToken = default)
        {
            const string consoleArgumentsParameter = "{console.arguments}";
            var processStartInfo = new ProcessStartInfo(consoleHost.Path, consoleHost.Arguments
                .Replace(consoleArgumentsParameter, arguments));

            if (workingDirectory != null)
            {
                processStartInfo.WorkingDirectory = workingDirectory;
            }

            logger.LogInformation("{0} {1}", processStartInfo.FileName, processStartInfo.Arguments);

            return StartProcess(processStartInfo, cancellationToken);
        }

        private Task StartProcess(
            ProcessStartInfo processStartInfo,
            CancellationToken cancellationToken)
        {
            var process = new Process
            {
                StartInfo = processStartInfo
            };

            process.Start();

            return process
                .WaitForExitAsync(cancellationToken);
        }

    }
}
