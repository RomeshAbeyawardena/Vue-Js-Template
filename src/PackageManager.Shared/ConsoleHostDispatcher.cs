using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared
{
    public class ConsoleHostDispatcher : IConsoleHostDispatcher
    {
        private readonly IConfiguration configuration;

        public ConsoleHostDispatcher(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConsoleHost DefaultConsoleHost => configuration.ConsoleHosts.Single(a => a.Default);

        public Task Dispatch(IConsoleHost consoleHost, string arguments)
        {
            const string consoleArgumentsParameter = "{console.arguments}";

            var processStartInfo = new ProcessStartInfo(consoleHost.Path, consoleHost.Arguments
                .Replace(consoleArgumentsParameter, arguments));

            return Process
                .Start(processStartInfo)
                .WaitForExitAsync();
        }
    }
}
