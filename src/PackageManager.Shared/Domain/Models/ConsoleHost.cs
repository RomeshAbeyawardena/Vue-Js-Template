using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Domain.Models
{
    public class ConsoleHost : IConsoleHost
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
