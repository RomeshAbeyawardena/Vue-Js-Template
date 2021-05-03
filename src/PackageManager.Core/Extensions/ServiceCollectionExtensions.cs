using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PackageManager.Shared;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Domain.Models;
using System;

namespace PackageManager.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,
            Action<ILoggingBuilder> buildLoggerFactory)
        {
            return services
                .AddSingleton<IConsole<ConsoleKeyInfo>, SystemConsole>()
                .AddSingleton<ISystemConsole, SystemConsole>()
                .AddMediatR(typeof(ServiceCollectionExtensions).Assembly)
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddSingleton(serviceProvider => LoggerFactory.Create(buildLoggerFactory))
                .AddSingleton<IConfigurationLoader, ConfigurationLoader>()
                .AddSingleton<IModuleLoader, ModuleLoader>()
                .AddSingleton<IConsoleHostDispatcher, ConsoleHostDispatcher>()
                .AddSingleton<IFileProvider, FileProvider>();
        }
    }
}
