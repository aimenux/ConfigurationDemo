using System;
using System.Collections.Generic;
using Lib.Configuration;
using Microsoft.Extensions.Options;

namespace Lib.Services;

public class ConfigurationOptionsService : IConfigurationService
{
    private readonly IOptions<Settings> _options;

    public ConfigurationOptionsService(IOptions<Settings> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public IDictionary<string, object> GetSettings()
    {
        var settings = _options.Value;
        var features = settings.Features;

        return new Dictionary<string, object>
        {
            [nameof(ServiceType)] = nameof(ConfigurationOptionsService),
            [nameof(Features.One)] = features.One,
            [nameof(Features.Two)] = features.Two,
        };
    }
}