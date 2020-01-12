using System.Collections.Generic;
using Lib.Configuration;
using Microsoft.Extensions.Options;

namespace Lib.Services
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
            var settings = _optionsSnapshot.Value;
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