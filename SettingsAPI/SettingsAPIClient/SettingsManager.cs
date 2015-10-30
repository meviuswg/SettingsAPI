using SettingsAPIClient.Provider;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingsManager
    {
        private static string _DEFAULT_DIR = "root";

        private string _apiKey;
        private string _url;

        public SettingsManager(string url, string apiKey)
        {
            Uri test;

            if (!Uri.TryCreate(url, UriKind.Absolute, out test))
            {
                throw new SettingsException("Invalid Uri");
            }
            this._url = url;

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new SettingsException("Invalid APIKey");
            }

            this._apiKey = apiKey;
        }

        #region Application

        public async Task<SettingsApplication[]> GetApplications()
        {
            var adminProvider = new AdminProvider(_url, _apiKey);
            return await adminProvider.Applications();
        }

        public async Task<SettingsApplication> GetApplication(string applicatinName)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicatinName);
            return await applicationProvider.Get();
        }

        public async Task<bool> CreateApplicationAsync(string applicationName, string description)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);

            return await adminProvider.CreateApplication(applicationName, description);
        }

        public async Task<bool> CreateApplicationVersionAsync(string applicationName, int version)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
            return await applicationProvider.CreateVerion(version);
        }

        public async Task<bool> UpdateApplicationAsync(string applicationName, string newApplicationName, string description)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);
            return await adminProvider.UpdateApplication(applicationName, newApplicationName, description);
        }

        public async Task<bool> DeleteApplicationAsync(string applicationName)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);
            return await adminProvider.DeleteApplication(applicationName);
        }

        public async Task<bool> DeleteApplicationVersionAsync(string applicationName, int version)
        {
            if (version == 1)
            {
                throw new SettingsException("Cannot delete version 1");
            }
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
            return await applicationProvider.DeleteVerion(version);
        }

        public async Task<bool> ApplicationExists(string name)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);
            return await adminProvider.ApplicationExists(name);
        }

        public async Task<bool> VersionExists(string applicationName, int version)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
            return await applicationProvider.VerionExists(version);
        }

        #endregion Application


        #region Admin

        public async Task<ApiKey> CreateApiKey(string applicationName, ApiKey key)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);
            return await adminProvider.CreateApiKey(applicationName, key);
        }

        public async Task<bool> ApiKeyExists(string applicationName, string apiKeyName)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);
            return await adminProvider.ApiKeyExists(applicationName, apiKeyName);
        }

        public async Task<ApiKey> GetApiKey(string applicationName, string apiKeyName)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);
            return await adminProvider.GetApiKey(applicationName, apiKeyName);
        }
        #endregion
        #region Direcotory

        public async Task<bool> CreateDirectoryAsync(string applicationName, string directoryName, string description)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
            return await applicationProvider.CreateDirectory(directoryName, description);
        }

        public async Task<bool> UpdateDirectoryAsync(string applicationName, string directoryName, string newDirectoryName, string description)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            return await applicationProvider.UpdateDirectory(directoryName, newDirectoryName, description);
        }

        public async Task<bool> CopyDirectoryAsync(string applicationName, string directoryName, string newDirectoryName)
        {
            return await CopyDirectoryAsync(applicationName, directoryName, newDirectoryName, 1);
        }

        public async Task<bool> CopyDirectoryAsync(string applicationName, string directoryName, string newDirectoryName, int version)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
            return await applicationProvider.CopyDirectory(directoryName, newDirectoryName, version);
        }

        public async Task<bool> DeleteDirectoryAsync(string applicationName, string directoryName)
        {
            if (string.Equals(directoryName, "root", StringComparison.CurrentCultureIgnoreCase) || string.Equals(directoryName, "root", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new SettingsException("This directory can not be deleted");
            }

            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
            return await applicationProvider.DeleteDirectory(directoryName);
        }

        public async Task<WorkingDirectoryObject> OpenDirectoryAsync(string applicationName)
        {
            return await OpenDirectoryAsync(applicationName, 1, _DEFAULT_DIR);
        }

        public async Task<WorkingDirectoryObject> OpenDirectoryAsync(string applicationName, string directory)
        {
            return await OpenDirectoryAsync(applicationName, 1, directory, -1);
        }

        public async Task<WorkingDirectoryObject> OpenDirectoryAsync(string applicationName, string directory, int objectId)
        {
            return await OpenDirectoryAsync(applicationName, 1, directory, objectId);
        }

        public async Task<WorkingDirectoryObject> OpenDirectoryAsync(string applicationName, int version, string directory)
        {
            return await OpenDirectoryAsync(applicationName, version, directory, -1);
        }

        public async Task<WorkingDirectoryObject> OpenDirectoryAsync(string applicationName, int version, string directoryName, int objectId)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
            var application = await applicationProvider.Get();

            if (application == null)
            {
                throw new SettingNotFoundException(directoryName);
            }

            var directory = application.Directories.SingleOrDefault(d => string.Equals(d.Name, directoryName));

            if (directory == null)
            {
                throw new SettingNotFoundException(directoryName);
            }

            var workingDirectory = new WorkingDirectoryObject(directory, applicationName, version, objectId, _url, _apiKey);

            workingDirectory.UseCache = true;
            workingDirectory.ExplicitlySave = false;
 
            await workingDirectory.Reload();

            return workingDirectory;
        }

        public async Task<bool> DirectoryExists(string applicationName, string directoryName)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
            return await applicationProvider.DirectoryExists(directoryName);
        }
    }

    #endregion Direcotory
}
 