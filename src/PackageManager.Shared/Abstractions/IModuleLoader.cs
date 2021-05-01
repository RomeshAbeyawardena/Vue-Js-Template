using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Abstractions
{
    public interface IModuleLoader
    {
        IEnumerable<IModule> GetModules(IEnumerable<Assembly> assemblies, 
            IConfiguration configuration);
    }
}
