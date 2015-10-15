using System.Collections.Generic;
using SettingsAPIData.Model;

namespace SettingsAPIData
{
    public interface IApiKeyRepository
    {
        IEnumerable<ApiKeyModel> GetApplicationApiKeys(string applicationName);
        ApiKeyModel GetKey(string apiKey);
    }
}