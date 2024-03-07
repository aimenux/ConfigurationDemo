using System.IO;
using System.Threading.Tasks;
using Lib.Configuration;
using Lib.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App;

public static class Program
{
    public  static Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureLogging((_, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
                loggingBuilder.Services.AddSingleton(serviceProvider =>
                {
                    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                    return loggerFactory.CreateLogger("ConfigurationAppDemo");
                });
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services.Configure<Features>(hostingContext.Configuration.GetSection(nameof(Features)));
                services.Configure<Settings>(hostingContext.Configuration.GetSection(nameof(Settings)));
                services.AddSingleton<IConfigurationService, ConfigurationOptionsService>();
                services.AddSingleton<IConfigurationService, ConfigurationOptionsMonitorService>();
                services.AddScoped<IConfigurationService, ConfigurationOptionsSnapshotService>();
                services.AddHostedService<ConfigurationHostedService>();
            })
            .UseConsoleLifetime();
        return builder.RunConsoleAsync();
    }
}