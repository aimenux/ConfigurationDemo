using System;
using System.Collections.Generic;
using Lib.Configuration;
using Microsoft.Extensions.Options;

namespace Lib.Services;

public class ConfigurationOptionsMonitorService : IConfigurationService
{
    private readonly IOptionsMonitor<Settings> _optionsMonitor;

    public ConfigurationOptionsMonitorService(IOptionsMonitor<Settings> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
    }

    public IDictionary<string, object> GetSettings()
    {
        var settings = _optionsMonitor.CurrentValue;
        var features = settings.Features;

        return new Dictionary<string, object>
        {
            [nameof(ServiceType)] = nameof(ConfigurationOptionsMonitorService),
            [nameof(Features.One)] = features.One,
            [nameof(Features.Two)] = features.Two,
        };
    }
}