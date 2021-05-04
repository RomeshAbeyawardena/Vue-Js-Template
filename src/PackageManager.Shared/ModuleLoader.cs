using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PackageManager.Shared
{
    public class ModuleLoader : IModuleLoader
    {
        private readonly IServiceProvider serviceProvider;

        public ModuleLoader(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<IModule> GetModules( IConfiguration configuration)
        {
            var assemblyTypes = new List<Type>();
            foreach (var module in configuration.Modules)
            {
                var assembly = Assembly//.Load(module.Assembly);
                    .LoadFrom($"{module.Assembly}.dll");
                
                assemblyTypes.Add(
                    assembly.GetType(module.Type));
            }

            return assemblyTypes.Select(t => t.Resolve<IModule>(serviceProvider));
        }
    }
}
