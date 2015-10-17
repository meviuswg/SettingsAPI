namespace SettingsAPIData
{
    public class SettingStore
    {
        private string _applicationName;
        private string _directory;
        private int _version;
        private int? _objectId;

        public SettingStore(string application, string directory)
        {
            _applicationName = application;
            _directory = directory;
            _version = 1;            
        }

        public SettingStore(string application, int version, string directory)
        {
            _applicationName = application;
            _directory = directory;
            _version = version;
            
        }


        public string ApplicationName { get { return _applicationName; } }
        public string DirectoryName { get { return _directory; } }
        public int Version { get { return _version; } }

    }
}