using System.Collections.Generic;
using Api.Configuration;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class ConfigurationOptionsSnapshotService : IConfigurationService
    {
        private readonly IOptionsSnapshot<Settings> _optionsSnapshot;

        public ConfigurationOptionsSnapshotService(IOptionsSnapshot<Settings> optionsSnapshot)
        {
            _optionsSnapshot = optionsSnapshot;
        }

        public IDictionary<string, object> GetSettings()
        {
            var features = _optionsSnapshot.Value.Features;

            return new Dictionary<string, object>
            {
                [nameof(Features.One)] = features.One,
                [nameof(Features.Two)] = features.Two
            };
        }
    }
}