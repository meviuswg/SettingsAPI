namespace SettingsAPIRepository
{
    public class SettingsDuplicateException : SettingsStoreException
    {
        public SettingsDuplicateException(string message) : base(message)
        {
        }
    }
}