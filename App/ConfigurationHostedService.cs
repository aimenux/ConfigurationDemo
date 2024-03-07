using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lib.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App;

public class ConfigurationHostedService : BackgroundService
{
    private readonly IEnumerable<IConfigurationService> _configurationServices;
    private readonly ILogger _logger;

    public ConfigurationHostedService(IEnumerable<IConfigurationService> configurationServices, ILogger logger)
    {
        _configurationServices = configurationServices ?? throw new ArgumentNullException(nameof(configurationServices));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                foreach (var configurationService in _configurationServices)
                {
                    Console.WriteLine($"\nSettings found with '{configurationService.GetType().Name}'");
                    var settings = configurationService.GetSettings();
                    foreach (var (key, value) in settings)
                    {
                        Console.WriteLine($"-> {key} = {value}");
                    }
                }
            }
        }
        catch(OperationCanceledException ex)
        {
            _logger.LogError(ex, "Configuration hosted service is stopping.");
        }
    }
}