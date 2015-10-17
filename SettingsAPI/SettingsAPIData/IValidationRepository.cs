using SettingsAPIData.Data; 

namespace SettingsAPIData
{
    public interface IValidationRepository
    {
        ApiKeyData GetKey(string apiKey); 
        void SetUsed(string apiKey); 
    }
}