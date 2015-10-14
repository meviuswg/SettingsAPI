using System.Threading.Tasks;

namespace SettingsAPIClient.Provider
{
    internal class DirectoryProvider : ApiClient
    {
        private string applicationName;
        private string directory; 

        public DirectoryProvider(string url, string apiKey, string applicationName, string directory) : base(url, apiKey)
        {
            this.applicationName = applicationName;
            this.directory = directory;
        }

        public async Task<SettingsDirectory> Get()
        {
            return await Get<SettingsDirectory>();
        }

        public async Task<bool> Create(string description)
        {
            return await Post<dynamic>(new { Name = directory, Description = description }, string.Concat("application", "/", applicationName, "/directories/"));
        }

        public new async Task<bool> Delete()
        {
            return await Delete(string.Empty);
        }

        public override string LocalPath { get { return string.Concat("application", "/", applicationName, "/directories/", directory); } } 
    }
}