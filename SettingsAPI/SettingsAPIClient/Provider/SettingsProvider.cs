namespace SettingsAPIClient.Provider
{
    internal class SettingsProvider : ApiClient<Setting[]>
    {
        private string applicationName;
        private string directory;
        private int objectId;
        private int version;

        public SettingsProvider(string url, string apiKey, string applicationName, int version, string directory, int objectId) : base(url, apiKey)
        {
            this.applicationName = applicationName;
            this.version = version;
            this.directory = directory;
            this.objectId = objectId;
        }

        public override string LoalPath { get { return string.Concat("settings", "/", applicationName, "/", version, "/", directory, "/", objectId); } }
    }
}