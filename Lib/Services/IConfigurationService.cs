using System.Collections.Generic;

namespace Lib.Services
{
    public interface IConfigurationService
    {
        IDictionary<string, object> GetSettings();
    }
}
