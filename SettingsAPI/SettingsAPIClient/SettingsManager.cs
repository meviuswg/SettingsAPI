using Newtonsoft.Json;
using SettingsAPIClient.Provider;
using SettingsAPIClient.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingsManager
    {
        private static string _DEFAULT_DIR = "root";

        private string _apiKey;
        private SettingsApplication _application;
        private ApplicationProvider _applicationProvider;
        private AdminProvider _adminProvider;
        private WorkingDirectoryObject _workingDirectory;
        private string _url;
        private int _currentVersion;
        private int _currentObjectId;
        private string _currentApplicationName;
 

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

        public SettingsApplication Application
        {
            get { return _application; }
        }

        public async Task<SettingsApplication[]> GetApplications()
        {
            _adminProvider = new AdminProvider(_url, _apiKey);
            return await _adminProvider.Applications();
        }

        public async Task<bool> CreateApplicationAsync(string applicationName, string description)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);

            if (await adminProvider.CreateApplication(applicationName, description))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CreateApplicationVersionAsync(string applicationName, int version)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await applicationProvider.CreateVerion(version))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateApplicationAsync(string applicationName, string newApplicationName, string description)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);

            if (await adminProvider.UpdateApplication(applicationName, newApplicationName, description))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteApplicationAsync(string applicationName)
        {
            var adminProvider = new AdminProvider(_url, _apiKey);

            if (await adminProvider.DeleteApplication(applicationName))
            {
                return true;
            }

            return true;
        }

        public async Task<bool> DeleteApplicationVersionAsync(string applicationName, int version)
        {
            if (version == 1)
            {
                throw new SettingsException("Cannot delete version 1");
            }
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await applicationProvider.DeleteVerion(version))
            {
                ResetCurrentWorkingDirectory();
                return await OpenApplicationAsync(applicationName);
            }

            return false;
        }

        public async Task<bool> OpenApplicationAsync(string applicationName)
        {
            return await OpenApplicationAsync(applicationName, 1);
        }

        public async Task<bool> OpenApplicationAsync(string applicationName, int version)
        {
            return await OpenDirectoryAsync(applicationName, version, _DEFAULT_DIR);
        }

        public async Task<bool> ApplicationExists(string name)
        {
            {
                return await _adminProvider.ApplicationExists(name);
            }
        }

        public async Task<bool> VersionExists(int version)
        {
            return await _applicationProvider.VerionExists(version);
        }


        #endregion Application

        #region Direcotory 

        public async Task<bool> CreateDirectoryAsync(string applicationName, string directoryName, string description)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await applicationProvider.CreateDirectory(directoryName, description))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateDirectoryAsync(string applicationName, string directoryName, string newDirectoryName, string description)
        {
            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await applicationProvider.UpdateDirectory(directoryName, newDirectoryName, description))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteDirectoryAsync(string applicationName, string directoryName)
        {
            if (string.Equals(directoryName, "root", StringComparison.CurrentCultureIgnoreCase) || string.Equals(directoryName, "root", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new SettingsException("This directory can not be deleted");
            }

            var applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await applicationProvider.DeleteDirectory(directoryName))
            {
                ResetCurrentWorkingDirectory();
            }

            return true;
        }

        public async Task<bool> OpenDirectoryAsync(string applicationName, string directory)
        {
            return await OpenDirectoryAsync(applicationName, 1, directory, 0);
        }

        public async Task<bool> OpenDirectoryAsync(string applicationName, string directory, int objectId)
        {
            return await OpenDirectoryAsync(applicationName, 1, directory, objectId);
        }

        public async Task<bool> OpenDirectoryAsync(string applicationName, int version, string directory)
        {
            return await OpenDirectoryAsync(applicationName, version, directory, 0);
        }

        public async Task<bool> OpenDirectoryAsync(string applicationName, int version, string directoryName, int objectId)
        {
            ResetCurrentWorkingDirectory();

            if (_application == null || string.Equals(_application.Name, applicationName, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                _applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);
                _application = await _applicationProvider.Get();

                _currentApplicationName = _application.Name;
                _currentVersion = version;
                _currentObjectId = objectId;
            }

            if (_application == null)
                return false;


            var directory = _application.Directories.SingleOrDefault(d => string.Equals(d.Name, directoryName));

            if (directory == null)
            {
                throw new SettingNotFoundException(directoryName);
            }

            _workingDirectory = new WorkingDirectoryObject(directory, applicationName, version, _url, _apiKey);

            _workingDirectory.UseCache = true;
            _workingDirectory.ExplicitlySave = false;
            _workingDirectory.ObjectID = objectId;

            return await _workingDirectory.Reload();

        }

        public WorkingDirectoryObject CurrentDirectory { get { return _workingDirectory; } }

        public async Task<bool> DirectoryExists(string name)
        {
            {
                return await _applicationProvider.DirectoryExists(name);
            }
        }

        #endregion Direcotory 

        /// <summary>
        /// Current application version
        /// </summary>
        public int Version
        {
            get
            {
                return _currentVersion;
            }
        }

        /// <summary>
        /// Current application name
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return _currentApplicationName;
            }
        }

        /// <summary>
        /// Current directory name
        /// </summary> 

        private void ResetCurrentWorkingDirectory()
        {
            _applicationProvider = null; 
            _workingDirectory = null;
            _application = null;
        }

     
    }
}