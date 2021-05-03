using PackageManager.Shared.Domain.Models;
using System.Collections.Generic;

namespace PackageManager.Shared.Abstractions
{
    public interface IConfiguration
    {
        string SolutionName { get; }
        string Output { get; }
        IEnumerable<string> ProjectNames { get; }
        IEnumerable<Module> Modules { get; set; }
        IEnumerable<Command> Commands { get; set; }
        IEnumerable<Output> Outputs { get; set; }
        IEnumerable<IConsoleHost> ConsoleHosts { get; set; }
        string ConfigurationPath { get; }
    }
}
