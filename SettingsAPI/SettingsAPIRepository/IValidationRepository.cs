using SettingsAPIRepository.Data; 

namespace SettingsAPIRepository
{
    public interface IValidationRepository
    {
        ApiKeyData GetKey(string apiKey); 
        void SetUsed(string apiKey); 
    }
}