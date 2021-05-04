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

            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardOutput = true;

            logger.LogTrace("{0} {1}", processStartInfo.FileName, processStartInfo.Arguments);

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
            var outputTask = Task.Run(async () =>
            {
                process.BeginOutputReadLine();
                process.OutputDataReceived += Process_OutputDataReceived;
                await process.WaitForExitAsync(cancellationToken);
            }, cancellationToken);

            var errorTask = Task.Run(async () =>
            {
                process.BeginErrorReadLine();
                process.ErrorDataReceived += Process_ErrorDataReceived;
                await process.WaitForExitAsync(cancellationToken);
            }, cancellationToken);
            
            return Task.WhenAll(outputTask, errorTask);
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                logger.LogError(e.Data);
            }
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                logger.LogInformation(e.Data);
            }
        }
    }
}
