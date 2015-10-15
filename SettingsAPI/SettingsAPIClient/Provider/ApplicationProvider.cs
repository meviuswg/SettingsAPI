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

        public async Task<bool> CreateVerion(int version)
        {
            return await Post(string.Empty, string.Format("/{0}/versions/{1}",LocalPath, version));
        }

        public async Task<bool> DeleteVerion(int version)
        {
            return await Delete(string.Format("/{0}/versions/{1}", LocalPath, version));
        }

        public new async Task<bool> Delete()
        {
            return await Delete(string.Empty);
        }

        public override string LocalPath { get { return string.Concat("application", "/", applicationName); } }
    }
}