using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Abstractions
{
    public interface IConsoleHost
    {
        bool Default { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        string Arguments { get; set; }
    }
}
