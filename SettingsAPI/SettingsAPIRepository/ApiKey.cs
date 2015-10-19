namespace SettingsAPIRepository
{
    public class ApiKey : IApiKey
    {
        private string _key;

        public ApiKey(string apiKey)
        {
            _key = apiKey;
        }

        public string Key
        {
            get
            {
                return _key;
            }
        }
    }
}