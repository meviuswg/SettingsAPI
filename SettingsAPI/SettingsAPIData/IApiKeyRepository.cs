using SettingsAPIData.Model;

namespace SettingsAPIData
{
    public interface IApiKeyRepository
    {
        ApiKeyModel GetKey(string apiKey);

        void SetUsed(string apiKey);

        void Invalidate(string apiKey);
    }
}