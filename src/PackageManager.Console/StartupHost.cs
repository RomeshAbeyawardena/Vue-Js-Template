using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Console
{
    public class StartupHost : IHost
    {
        private Startup startup;
        public StartupHost(IServiceCollection services)
        {
            Services = services
                .AddSingleton<Startup>()
                .BuildServiceProvider();
        }

        public StartupHost(Func<IServiceCollection, IServiceCollection> servicesAction)
            : this(servicesAction(new ServiceCollection()))
        {
            
        }

        public IServiceProvider Services { get; }

        public void Dispose()
        {
            startup.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            startup = Services.GetRequiredService<Startup>();
            return startup.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            return startup.StopAsync(cancellationToken);
        }
    }
}
