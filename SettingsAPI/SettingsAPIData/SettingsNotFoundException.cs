namespace SettingsAPIData
{
    public class SettingsNotFoundException : SettingsStoreException
    {
        public SettingsNotFoundException(string message) : base(message)
        {
        }
    }
}