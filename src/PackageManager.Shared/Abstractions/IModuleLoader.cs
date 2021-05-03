using System.Collections.Generic;

namespace PackageManager.Shared.Abstractions
{
    public interface IModuleLoader
    {
        IEnumerable<IModule> GetModules(IConfiguration configuration);
    }
}
