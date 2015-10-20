using System;
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

        #region Applications

        public async Task<SettingsApplication> Get()
        {
            return await Get<SettingsApplication>();
        }

        #endregion

        #region Versions

        public async Task<bool> CreateVerion(int version)
        {
            return await Post(string.Empty, string.Format("/{0}/versions/{1}", LocalPath, version));
        }

        public async Task<bool> DeleteVerion(int version)
        {
            return await Delete(string.Format("/{0}/versions/{1}", LocalPath, version));
        }

        public async Task<SettingsVersion> GetVerion(int version)
        {
            return await Get<SettingsVersion>(string.Format("/{0}/versions/{1}", LocalPath, version));
        }

        public async Task<bool> VerionExists(int version)
        {
            try
            {
                await GetVerion(version);
                return false;
            }
            catch (SettingNotFoundException ex)
            {
                return false;
            }
        }


        #endregion

        #region Directories

        public async Task<SettingsDirectory> GetDirectory(string name)
        {
            return await Get<SettingsDirectory>(string.Format("/{0}/directories/{1}", LocalPath, name));
        }

        public async Task<bool> CreateDirectory(string name)
        {
            return await Post(string.Empty, string.Format("/{0}/directories/{1}", LocalPath, name));
        }

        public async Task<bool> CopyDirectory(string fromName, string toName)
        {
            return await CopyDirectory(fromName, toName, 1);
        }

        public async Task<bool> CopyDirectory(string fromName, string toName, int version)
        {
            return await Post(string.Empty, string.Format("/{0}/directories/copy/{1}/{2}/{3}", LocalPath, fromName, version, toName));
        }

        public async Task<bool> CreateDirectory(string name, string description)
        {
            return await Post<SettingsDirectory>(new SettingsDirectory { Name = name, Description = description }, string.Format("/{0}/directories/{1}", LocalPath, name));
        }

        public async Task<bool> UpdateDirectory(string directoryName, string newDirectoryName, string description)
        {
            return await Put(new SettingsDirectory { Name = newDirectoryName, Description = description }, string.Format("/{0}/directories/{1}", LocalPath, directoryName));
        }

        public async Task<bool> DeleteDirectory(string name)
        {
            return await Delete(string.Format("/{0}/directories/{1}", LocalPath, name));
        }

        public async Task<bool> DirectoryExists(string name)
        {
            try
            {
                await GetDirectory(name);
                return true;
            }
            catch (SettingNotFoundException)
            {
                return false;
            }

        }

        #endregion

        public override string LocalPath { get { return string.Concat("application", "/", applicationName); } }


    }
}