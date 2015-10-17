using System.Collections.Generic;
using SettingsAPIData.Model;

namespace SettingsAPIData
{
    public interface IApiKeyRepository
    {
        IEnumerable<ApiKeyModel> GetApplicationApiKeys(string applicationName);
        ApiKeyModel GetApiKey(string applicationName, string apiKey);
        ApiKeyModel CreateApiKey(string applicationName, SaveApiKeyModel model);
        void UpdateApiKey(string applicationName, SaveApiKeyModel model);
        void DeleteApiKey(string applicationName, string apiKey);
    }
}