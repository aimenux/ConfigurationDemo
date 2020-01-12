using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lib.Services;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class ConfigurationHostedService : IHostedService
    {
        private readonly IEnumerable<IConfigurationService> _configurationServices;

        public ConfigurationHostedService(IEnumerable<IConfigurationService> configurationServices)
        {
            _configurationServices = configurationServices;
        }

        public Task StartAsync(CancellationToken cancellationToken)
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

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
