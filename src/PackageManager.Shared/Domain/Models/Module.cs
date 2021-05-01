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
        public bool Default { get; set; }
        public bool Enabled { get; set; }
        public string Assembly { get; set; }
        
        public string Type { get; set; }
    }
}
