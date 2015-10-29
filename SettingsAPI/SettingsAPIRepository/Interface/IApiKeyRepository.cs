using System.Collections.Generic;
using SettingsAPIRepository.Model;
using SettingsAPIRepository.Data;

namespace SettingsAPIRepository
{
    public interface IApiKeyRepository
    {
        IEnumerable<ApiKeyModel> GetApplicationApiKeys(string applicationName);
        ApiKeyModel GetApiKey(string applicationName, string name); 
        ApiKeyModel CreateApiKey(string applicationName, SaveApiKeyModel model);
        void UpdateApiKey(string applicationName, SaveApiKeyModel model);
        void DeleteApiKey(string applicationName, string name);
    }
}