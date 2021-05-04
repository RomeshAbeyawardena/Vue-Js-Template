using Microsoft.Extensions.Logging;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Extensions;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Console
{
    public class Startup : IStartup
    {
        private readonly IConfiguration configuration;
        private readonly IConfigurationLoader configurationLoader;
        private readonly IModuleLoader moduleLoader;
        private readonly ILogger<Startup> logger;
        private IModule currentModule;
        public Startup(IConfiguration configuration,
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
                .LoadConfigurationFromXml(configuration, Path.Combine(typeof(Startup).GetLocation(), "config.xml"));

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
