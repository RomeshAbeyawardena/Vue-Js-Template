using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Console
{
    public class Startup : IStartup
    {
        private readonly Shared.Abstractions.IConfiguration configuration;
        private readonly IConfigurationLoader configurationLoader;

        public Startup(Shared.Abstractions.IConfiguration configuration,
            IConfigurationLoader configurationLoader)
        {
            this.configuration = configuration;
            this.configurationLoader = configurationLoader;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            configurationLoader.LoadConfigurationFromXml(configuration, "config.xml");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
