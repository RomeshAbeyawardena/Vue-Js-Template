using PackageManager.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        string ConfigurationPath { get; }
    }
}
