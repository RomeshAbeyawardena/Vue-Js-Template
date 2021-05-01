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
        string ProjectName { get; }
        IEnumerable<Module> Modules { get; }
        IEnumerable<Command> Commands { get; }
        IEnumerable<File> Files { get; }
        IEnumerable<FileExtension> FileExtensions { get; }
    }
}
