using System.Threading.Tasks;

namespace SettingsAPIClient.Provider
{
    internal class AdminProvider : ApiClient
    {
        public AdminProvider(string url, string apiKey) : base(url, apiKey)
        {
        }

        public override string LocalPath { get { return string.Concat("admin", "/"); } }

        public async Task<bool> ApplicationExists(string applicationName)
        {
            try
            {
                await Get<SettingsApplication>(string.Concat(LocalPath, applicationName));
                return true;
            }
            catch (SettingNotFoundException)
            {
                return false;
            }
        }

        public async Task<SettingsApplication[]> Applications()
        {
            return await Get<SettingsApplication[]>();
        }

        public async Task<bool> CreateApplication(string applicationName, string description)
        {
            return await Post<SettingsApplication>(new SettingsApplication { Name = applicationName, Description = description });
        }

        public async Task<bool> UpdateApplication(string applicationName, string newApplicatinName, string description)
        {
            return await Put(new SettingsApplication { Name = newApplicatinName, Description = description }, string.Concat(LocalPath, "/", applicationName));
        }

        public async Task<bool> DeleteApplication(string applicationName)
        {
            return await Delete(string.Concat(LocalPath, applicationName));
        }
    }
}