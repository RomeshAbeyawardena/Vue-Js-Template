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
        private readonly IModuleLoader moduleLoader;
        private IModule currentModule;
        public Startup(Shared.Abstractions.IConfiguration configuration,
            IConfigurationLoader configurationLoader,
            IModuleLoader moduleLoader)
        {
            this.configuration = configuration;
            this.configurationLoader = configurationLoader;
            this.moduleLoader = moduleLoader;
        }

        public void Dispose()
        {
            StopAsync().Wait();
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            configurationLoader
                .LoadConfigurationFromXml(configuration, "config.xml");

            var modules = moduleLoader.GetModules(null, configuration);

            foreach (var module in modules)
            {
                currentModule = module;
                await module
                    .RunAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            return currentModule?
                .DisposeAsync()
                .AsTask();
        }
    }
}
