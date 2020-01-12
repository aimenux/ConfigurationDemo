using System.Collections.Generic;
using Lib.Configuration;
using Microsoft.Extensions.Options;

namespace Lib.Services
{
    public class ConfigurationOptionsService : IConfigurationService
    {
        private readonly IOptions<Settings> _options;

        public ConfigurationOptionsService(IOptions<Settings> options)
        {
            _options = options;
        }

        public IDictionary<string, object> GetSettings()
        {
            var settings = _options.Value;
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