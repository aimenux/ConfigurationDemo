using Asp.Versioning;
using Lib.Configuration;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ConfigurationsController : ControllerBase
{
    private readonly Func<ServiceType, IConfigurationService> _serviceResolver;
    private readonly ILogger _logger;

    public ConfigurationsController(Func<ServiceType, IConfigurationService> serviceResolver, ILogger logger)
    {
        _serviceResolver = serviceResolver ?? throw new ArgumentNullException(nameof(serviceResolver));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("{serviceType}")]
    public IDictionary<string, object> GetSettings(ServiceType serviceType)
    {
        return _serviceResolver(serviceType).GetSettings();
    }
}