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
        private SettingsDirectory _directory;
        private DirectoryProvider _directoryProvider;
        private Dictionary<string, Setting> _items;
        private SettingsProvider _settingsProvider;
        private string _url;
        private int _currentVersion;
        private int _currentObjectId;
        private string _currentApplicationName;
        private string _currentDirectoryName;
        public SettingsManager(string url, string apiKey)
        {
            Uri test;

            if(!Uri.TryCreate(url, UriKind.Absolute, out test))
            {
                throw new SettingsException("Invalid Uri");
            }
            this._url = url;


            if(string.IsNullOrWhiteSpace(apiKey))
            {
                throw new SettingsException("Invalid APIKey");
            }


            this._apiKey = apiKey;

            _items = new Dictionary<string, Setting>();
            ExplicitlySave = false;
            UseCache = true;
        }

        /// <summary>
        /// When True, settings are only saved when Save is explicitly called
        /// </summary>
        public bool ExplicitlySave { get; set; }

        /// <summary>
        /// When True, settings are written to the remote store, but read from the internal cache.
        /// </summary>
        public bool UseCache { get; set; }

        #region Application

        public SettingsApplication Application
        {
            get { return _application; }
        }

        public async Task<bool> CreateApplicationAsync(string applicationName, string description)
        {
            _applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await _applicationProvider.Create(description))
            {
                return await OpenApplicationAsync(applicationName);
            }

            return false;
        }

        public async Task<bool> CreateApplicationVersionAsync(string applicationName, int version)
        {
            _applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await _applicationProvider.CreateVerion(version))
            {
                ClearCurrentWorkingDirectory();
                return await OpenApplicationAsync(applicationName);
            }

            return false;
        }

        public async Task<bool> DeleteApplicationAsync(string applicationName)
        {
            _applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await _applicationProvider.Delete())
            {
                ClearCurrentWorkingDirectory();
            }

            return true;
        }

        public async Task<bool> DeleteApplicationVersionAsync(string applicationName, int version)
        {
            _applicationProvider = new ApplicationProvider(_url, _apiKey, applicationName);

            if (await _applicationProvider.DeleteVerion(version))
            {
                ClearCurrentWorkingDirectory();
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
        private void ClearCurrentWorkingDirectory()
        {
            _applicationProvider = null;
            _directoryProvider = null;
            _directory = null;
            _items = null;
            _application = null;
        }
        #endregion Application

        #region Direcotory

        public SettingsDirectory Directory
        {
            get { return _directory; }
        }

        public async Task<bool> CreateDirectoryAsync(string applicationName, string directoryName, string description)
        {
            _directoryProvider = new DirectoryProvider(_url, _apiKey, applicationName, directoryName);

            if (await _directoryProvider.Create(description))
            {
                return await OpenDirectoryAsync(applicationName, directoryName);
            }

            return false;
        }

        public async Task<bool> DeleteDirectoryAsync(string applicationName, string directoryName)
        {
            _directoryProvider = new DirectoryProvider(_url, _apiKey, applicationName, directoryName);

            if (await _directoryProvider.Delete())
            {
                ClearCurrentWorkingDirectory();
            }

            return true;
        }

        public bool OpenDirectory(string applicationName, string directory)
        {
            try
            {
                return Task.Run(() => OpenDirectoryAsync(applicationName, 1, directory, 0)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
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

        public async Task<bool> OpenDirectoryAsync(string applicationName, int version, string directory, int objectId)
        {
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

            _directoryProvider = new DirectoryProvider(_url, _apiKey, applicationName, directory);

            _directory = await _directoryProvider.Get();
            _currentDirectoryName = _directory.Name;

            if (_directory != null)
            {
                _settingsProvider = new SettingsProvider(_url, _apiKey, applicationName, version, directory, objectId);
                return await Reload();
            }
            else
            {
                if (string.Equals(directory, _DEFAULT_DIR))
                    throw new SettingsException(string.Format("Failed to open application '{0}' version {1}. The target does not exist or you are not authorized to access it", applicationName, version));
                else
                    throw new SettingsException(string.Format("Failed to open directory '{0}' of application '{1}' version {2}. The target does not exist or you are not authorized to access it", directory, applicationName, version));
            }
 
        }
        #endregion Direcotory

        #region Settings

        public IEnumerable<Setting> Items
        {
            get { return _items.Values.ToArray(); }
        }

        public async Task<Setting[]> GetAsync()
        {
            return await _settingsProvider.Get();
        }

        public async Task<Nullable<bool>> GetBooleanAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToBoolean(s.Value);

            return null;
        }

        public async Task<byte[]> GetByteArrayAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return SerializationHelper.FromBase64String(s.Value);

            return null;
        }

        public async Task<Nullable<DateTime>> GetDateTimeAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToDateTime(s.Value);

            return null;
        }

        public async Task<Nullable<double>> GetDoubleAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToDouble(s.Value);

            return null;
        }

        public async Task<Image> GetImageAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return SerializationHelper.ToImage(s.Value);

            return null;
        }

        public async Task<Nullable<int>> GetIntAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToInt32(s.Value);

            return null;
        }

        public async Task<T> GetKeyAsync<T>(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return JsonConvert.DeserializeObject<T>(s.Value);

            return default(T);
        }

        public async Task<Setting> GetKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            if(UseCache)
            {
                if (_items.ContainsKey(key))
                {
                    return _items[key];
                }
                else
                {
                    throw new SettingNotFoundException(key);
                }
            }
            else
            {
                var result = await _settingsProvider.Get(key);

                if(result.Count() == 0)
                { 
                    throw new SettingNotFoundException(key);
                }
                else
                {
                    var setting = result.Single();

                    _items[setting.Key] = setting;

                    return setting;
                }
            }
        }

        public async Task<string> GetStringAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToString(s.Value);

            return null;
        }

        public async Task<bool> SaveAsync(string key, bool value)
        {
            return await SaveAsync(key, value.ToString());
        }

        public async Task<bool> SaveAsync(string key, DateTime value)
        {
            return await SaveAsync(key, value.ToString());
        }

        public async Task<bool> SaveAsync(string key, decimal value)
        {
            return await SaveAsync(key, value.ToString());
        }

        public async Task<bool> SaveAsync(string key, Image value)
        {
            return await SaveAsync(key, SerializationHelper.ImageToString(value));
        }

        public async Task<bool> SaveAsync(string key, byte[] value)
        {
            return await SaveAsync(key, SerializationHelper.ToBase64String(value));
        }

        public async Task<bool> SaveAsync(IEnumerable<Setting> settings)
        {
            if(ExplicitlySave)
            {
                SetInternalItemsCollection(settings);
                return true;
            }

            bool settingsSaved = await _settingsProvider.Save(settings);

            if (settingsSaved)
            {
               SetInternalItemsCollection(settings);
            }

            return settingsSaved;
        }

        private void SetInternalItemsCollection(IEnumerable<Setting> settings)
        {
            foreach (var item in settings)
            {
                _items[item.Key] = item;
            }
        }

        public async Task<bool> SaveAsync(Setting setting)
        {
           return await SaveAsync(new Setting[] { setting }); 
        }

        public async Task<bool> SaveAsync(string key, string value)
        {
            return await SaveAsync(new Setting { Key = key, Value = value });
        }

        /// <summary>
        /// Saves all items to the remote store
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
           return await  _settingsProvider.Save(_items.Values.ToList());
        } 

        /// <summary>
        /// Reloads all the settings of the current directory.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Reload()
        {
            var settingsSet = await _settingsProvider.Get();

            _items = new Dictionary<string, Setting>();

            SetInternalItemsCollection(settingsSet);

            return true;
        }
        #endregion Settings

        /// <summary>
        /// Current settings object Id. The default objectId is 0. All settings are read and writting against this objectId.
        /// </summary>
        public int ObjectID
        {
            get
            {
                return _currentObjectId;
            }
        }

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
        public string DirectoryName
        {
            get
            {
                return _currentDirectoryName;
            }
        }

        /// <summary>
        /// Returns true if the current directory allowes settings to be updated for this APIKey.
        /// </summary>
        public bool AllowWrite
        {
            get
            {
                if (_directory != null)
                {
                    return _directory.AllowWrite;
                }
                else
                {
                    throw new SettingsException("No open directory.");
                }
            }
        }

        /// <summary>
        /// Returns true if the current directory allowes settings to be created for this APIKey.
        /// </summary>
        public bool AllowCreate
        {
            get
            {
                if (_directory != null)
                {
                    return _directory.AllowCreate;
                }
                else
                {
                    throw new SettingsException("No open directory.");
                }
            }
        }

        /// <summary>
        /// Returns true if the current directory allowes settings to be deleted for this APIKey.
        /// </summary>
        public bool AllowDelete
        {
            get
            {
                if (_directory != null)
                {
                    return _directory.AllowCreate;
                }
                else
                {
                    throw new SettingsException("No open directory.");
                }
            }
        }

        public async Task<bool> ExistsAsync(string key)
        {
            if(UseCache)
            {
                return _items.ContainsKey(key);
            }
            else
            {
                return await _settingsProvider.Exists(key);
            }
        }
    }
}