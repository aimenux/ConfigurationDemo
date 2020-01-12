using System;
using System.ComponentModel;
using Api.Configuration;
using Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);
                })
                .ConfigureLogging((hostingContext, loggingBuilder) =>
                {
                    loggingBuilder.Services.AddSingleton(serviceProvider =>
                    {
                        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                        return loggerFactory.CreateLogger("ConfigurationApiDemo");
                    });
                    loggingBuilder.AddConsole();
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddTransient(GetServiceTypeFactory());
                    services.AddSingleton<ConfigurationOptionsService>();
                    services.AddSingleton<ConfigurationOptionsMonitorService>();
                    services.AddScoped<ConfigurationOptionsSnapshotService>();
                    services.Configure<Features>(hostingContext.Configuration.GetSection(nameof(Features)));
                    services.Configure<Settings>(hostingContext.Configuration.GetSection(nameof(Settings)));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static Func<IServiceProvider, Func<ServiceType, IConfigurationService>> GetServiceTypeFactory()
        {
            return serviceProvider => serviceType =>
            {
                return serviceType switch
                {
                    ServiceType.ConfigurationOptionsService => serviceProvider.GetService<ConfigurationOptionsService>(),
                    ServiceType.ConfigurationOptionsMonitorService => serviceProvider.GetService<ConfigurationOptionsMonitorService>(),
                    ServiceType.ConfigurationOptionsSnapshotService => serviceProvider.GetService<ConfigurationOptionsSnapshotService>(),
                    ServiceType.None => throw new InvalidEnumArgumentException(nameof(ServiceType.None)),
                    _ => throw new ArgumentOutOfRangeException($"Unexpected service type {serviceType}")
                };
            };
        }
    }
}
