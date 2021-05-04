using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Domain.Models;
using System.Collections.Generic;

namespace PackageManager.Shared
{
    public class Configuration : IConfiguration
    {
        public Configuration(string solutionName,
            string output,
            IEnumerable<string> projectNames,
            string configurationPath)
        {
            SolutionName = solutionName;
            Output = output;
            ProjectNames = projectNames;
            ConfigurationPath = configurationPath;
        }

        public string SolutionName { get; }

        public string Output { get; }

        public string ConfigurationPath { get; }

        public IEnumerable<string> ProjectNames { get; }

        public IEnumerable<Module> Modules { get; set; }

        public IEnumerable<Command> Commands { get; set; }

        public IEnumerable<Output> Outputs { get; set; }

        public IEnumerable<IConsoleHost> ConsoleHosts { get; set; }
    }
}
