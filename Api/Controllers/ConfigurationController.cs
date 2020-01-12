using System;
using System.Collections.Generic;
using Api.Configuration;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly Func<ServiceType, IConfigurationService> _serviceResolver;

        public ConfigurationController(Func<ServiceType, IConfigurationService> serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }

        [HttpGet("{serviceType}")]
        public IDictionary<string, object> GetSettings(ServiceType serviceType)
        {
            return _serviceResolver(serviceType).GetSettings();
        }
    }
}
