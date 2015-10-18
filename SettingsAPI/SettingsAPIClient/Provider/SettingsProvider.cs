using System.Collections.Generic;
using System.Threading.Tasks;

namespace SettingsAPIClient.Provider
{
    internal class SettingsProvider : ApiClient 
    {
        private string applicationName;
        private string directory;
        private int version;

        public SettingsProvider(string url, string apiKey, string applicationName, int version, string directory) : base(url, apiKey)
        {
            this.applicationName = applicationName;
            this.version = version;
            this.directory = directory; 
        }


        public async Task<bool> Save(IEnumerable<Setting> settings)
        {
            return await Post<IEnumerable<Setting>>(settings);
        }

        public async Task<Setting[]> Get()
        {
            return await Get<Setting[]>();
        }

        public async Task<Setting[]> Get(int objectId, string key)
        {
            return await Get<Setting[]>(string.Concat(LocalPath, "/", objectId, "/", key));
        }

        public async Task<bool> Delete(int objectId, string key)
        {
            return await Post<string>(string.Concat(LocalPath, "/", objectId, "/", key));
        }

        public async Task<bool> Exists(int objectId, string key)
        {
            try
            {
                var result = await Get(objectId,key);
                return true;
            }
            catch (SettingNotFoundException)
            {
                return false;
            }      
        }

        public override string LocalPath { get { return string.Concat("settings", "/", applicationName, "/", version, "/", directory); } }
    }
}