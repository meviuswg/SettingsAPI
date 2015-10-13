using System.Threading.Tasks;

namespace SettingsAPIClient.Provider
{
    internal class DirectoryProvider : ApiClient<SettingsDirectory>
    {
        private string applicationName;
        private string directory;
      

        public DirectoryProvider(string url, string apiKey, string applicationName, string directory) : base(url, apiKey)
        {
            this.applicationName = applicationName;
            this.directory = directory;
        }

        public override string LoalPath { get { return string.Concat("application", "/", applicationName, "/directories/", directory); } } 
    }
}