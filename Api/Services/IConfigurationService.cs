using System.Collections.Generic;

namespace Api.Services
{
    public interface IConfigurationService
    {
        IDictionary<string, object> GetSettings();
    }
}
