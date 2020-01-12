using System.Collections.Generic;
using Api.Configuration;
using Microsoft.Extensions.Options;

namespace Api.Services
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
            var features = _optionsMonitor.CurrentValue.Features;

            return new Dictionary<string, object>
            {
                [nameof(Features.One)] = features.One,
                [nameof(Features.Two)] = features.Two
            };
        }
    }
}