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

        public async Task<SettingsApplication[]> GetAll()
        {
            return await Get<SettingsApplication[]>("admin");
        }

        public async Task<bool> Create(string description)
        {
           return await Post<dynamic>(new { Name = applicationName, Description = description }, "admin");
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
            return await Delete(string.Format("admin/{0}", applicationName));
        }

        public async Task<bool> Exists(string name)
        {
            try
            {
                await Get<SettingsApplication>(string.Concat("application", "/", name));
                return true;
            }
            catch (SettingNotFoundException)
            {
                return false;
            }
          
        }

        public override string LocalPath { get { return string.Concat("application", "/", applicationName); } }
    }
}