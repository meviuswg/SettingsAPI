using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override string LoalPath { get { return string.Concat("settings", "/", applicationName, "/", version, "/", directory, "/", objectId); } }

        

   
    }
}
