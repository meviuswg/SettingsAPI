namespace SettingsAPIRepository
{
    public class SettingsNotFoundException : SettingsStoreException
    {
        public SettingsNotFoundException(string message) : base(message)
        {
        }
    }
}