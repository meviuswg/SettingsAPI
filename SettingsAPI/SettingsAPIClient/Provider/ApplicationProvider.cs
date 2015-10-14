using System.Threading.Tasks;

namespace SettingsAPIClient.Provider
{
    internal class ApplicationProvider : ApiClient
    {
        private string applicationName;

        public ApplicationProvider(string url, string apiKey, string applicationName) : base(url, apiKey)
        {
            this.applicationName = applicationName;
        }

        public async Task<SettingsApplication> Get()
        {
            return await Get<SettingsApplication>();
        }

        public async Task<bool> Create(string description)
        {
           return await Post<dynamic>(new { Name = applicationName, Description = description }, "application");
        }

        public new async Task<bool> Delete()
        {
            return await Delete(string.Empty);
        }

        public override string LocalPath { get { return string.Concat("application", "/", applicationName); } }
    }
}