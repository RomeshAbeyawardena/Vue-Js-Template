using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<Startup> logger;
        private IModule currentModule;
        public Startup(Shared.Abstractions.IConfiguration configuration,
            IConfigurationLoader configurationLoader,
            IModuleLoader moduleLoader,
            ILogger<Startup> logger)
        {
            this.configuration = configuration;
            this.configurationLoader = configurationLoader;
            this.moduleLoader = moduleLoader;
            this.logger = logger;
        }

        public void Dispose()
        {
            StopAsync().Wait();
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            configurationLoader
                .LoadConfigurationFromXml(configuration, "config.xml");

            logger.LogInformation("Added configuration from config.xml");

            var modules = moduleLoader.GetModules(configuration);

            foreach (var module in modules)
            {
                currentModule = module;
                await currentModule
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
