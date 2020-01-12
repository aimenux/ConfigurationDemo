using System.Collections.Generic;
using Api.Configuration;
using Microsoft.Extensions.Options;

namespace Api.Services
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
            var features = _options.Value.Features;

            return new Dictionary<string, object>
            {
                [nameof(Features.One)] = features.One,
                [nameof(Features.Two)] = features.Two
            };
        }
    }
}