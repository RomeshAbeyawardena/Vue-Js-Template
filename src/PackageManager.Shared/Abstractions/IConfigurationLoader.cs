using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Abstractions
{
    public interface IConfigurationLoader
    {
        IConfiguration LoadConfigurationFromXml(IConfiguration configuration,
            string defaultConfigurationPath = default);
    }
}
