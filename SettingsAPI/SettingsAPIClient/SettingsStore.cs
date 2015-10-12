namespace SettingsAPIClient
{
    public class SettingsStore
    {
        public SettingsStore(string applicationName, string storeName) : this(applicationName, 1, storeName)
        { }

        public SettingsStore(string applicationName, int version, string storeName) : this(applicationName, 1, storeName, null)
        {
        }

        public SettingsStore(string applicationName, int version, string storeName, int? objectId)
        {
            this.Application = applicationName;
            this.Version = version;
            this.Name = storeName;
            this.ObjectId = objectId;
        }

        public string Application { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public int? ObjectId { get; set; }
    }
}