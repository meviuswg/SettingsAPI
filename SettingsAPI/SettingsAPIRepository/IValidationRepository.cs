using SettingsAPIRepository.Data; 

namespace SettingsAPIRepository
{
    public interface IValidationRepository
    {
        ApiKeyData GetApiKey(string apiKey);
        bool IsValid(string apiKey, out int keyId); 
        void SetUsed(string apiKey); 
    }
}