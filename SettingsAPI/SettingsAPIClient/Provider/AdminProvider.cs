using System.Threading.Tasks;

namespace SettingsAPIClient.Provider
{
    internal class AdminProvider : ApiClient
    {
        public AdminProvider(string url, string apiKey) : base(url, apiKey)
        {
        }

        public override string LocalPath { get { return string.Concat("admin"); } }

        public async Task<bool> ApplicationExists(string applicationName)
        {
            return await Exists(string.Concat(LocalPath , "/", applicationName));
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

        public async Task<bool> CopyApplication(string applicationName, string newApplicatinName, string description)
        {
            return await CopyApplication(applicationName, 0, newApplicatinName, description);
        }
        public async Task<bool> CopyApplication(string applicationName, int copyVersion, string newApplicatinName, string description)
        {
            return await Post(new SettingsApplication { Name = newApplicatinName, Description = description }, string.Concat(LocalPath, "/", applicationName, "/copy/", copyVersion));
        }
        public async Task<bool> DeleteApplication(string applicationName)
        {
            return await Delete(string.Concat(LocalPath, "/", applicationName));
        }
    }
}