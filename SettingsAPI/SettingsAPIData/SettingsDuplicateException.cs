namespace SettingsAPIData
{
    public class SettingsDuplicateException : SettingsStoreException
    {
        public SettingsDuplicateException(string message) : base(message)
        {
        }
    }
}