using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Domain.Models
{
    public class Module
    {
        public bool Enabled { get; set; }
        public string AssemblyName  { get; set; }
        public Assembly Assembly => Assembly.LoadFrom(AssemblyName);

        public string Type { get; set; }
    }
}
