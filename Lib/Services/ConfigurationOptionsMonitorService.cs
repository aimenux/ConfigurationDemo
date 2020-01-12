using System.Collections.Generic;
using Lib.Configuration;
using Microsoft.Extensions.Options;

namespace Lib.Services
{
    public class ConfigurationOptionsMonitorService : IConfigurationService
    {
        private readonly IOptionsMonitor<Settings> _optionsMonitor;

        public ConfigurationOptionsMonitorService(IOptionsMonitor<Settings> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        public IDictionary<string, object> GetSettings()
        {
            var settings = _optionsMonitor.CurrentValue;
            var serviceType = settings.ServiceType;
            var features = settings.Features;

            return new Dictionary<string, object>
            {
                [nameof(ServiceType)] = serviceType,
                [nameof(Features.One)] = features.One,
                [nameof(Features.Two)] = features.Two,
            };
        }
    }
}