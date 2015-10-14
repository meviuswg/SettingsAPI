using System.Collections.Generic;
using System.Threading.Tasks;

namespace SettingsAPIClient.Provider
{
    internal class SettingsProvider : ApiClient 
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

        public async Task<bool> Save(IEnumerable<Setting> settings)
        {
            return await Post<IEnumerable<Setting>>(settings);
        }

        public async Task<Setting[]> Get()
        {
            return await Get<Setting[]>();
        }

        public async Task<Setting[]> Get(string key)
        {
            return await Get<Setting[]>(string.Concat(LocalPath, "/", key));
        }

        public async Task<bool> Delete(string key)
        {
            return await Post<string>(string.Concat(LocalPath, "/", key));
        }

        public override string LocalPath { get { return string.Concat("settings", "/", applicationName, "/", version, "/", directory, "/", objectId); } }
    }
}