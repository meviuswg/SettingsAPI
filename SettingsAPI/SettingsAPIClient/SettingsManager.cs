using Newtonsoft.Json;
using SettingsAPIClient.Provider;
using SettingsAPIClient.Util;
using System;
using System.Collections;
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
        private SettingsProvider _settingsProvider;
        private Setting[] _items;
        private string _url;

        public SettingsManager(string url, string apiKey)
        {
            this._url = url;
            this._apiKey = apiKey;
        }

        public bool UseCache { get; set; }

        public bool ExplicitlySave { get; set; }

        #region Application

        public bool OpenApplication(string applicationName)
        {
            try
            {
                Task.Run(() => OpenApplicationAsync(applicationName)).Wait();
                return _application != null;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<bool> OpenApplicationAsync(string applicationName)
        {
            return await OpenDirectoryAsync(applicationName, 1, _DEFAULT_DIR);
        }

        public bool CreateApplication(string applicationName, string description)
        {
            try
            {
                return Task.Run(() => CreateApplicationAsync(applicationName, description)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
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

        public bool DeleteApplication(string applicationName)
        {
            try
            {
                return Task.Run(() => DeleteApplicationAsync(applicationName)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
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

      

        private void ClearCurrentWorkingDirectory()
        {
            _applicationProvider = null;
            _directoryProvider = null;
            _directory = null;
            _items = null;
            _application = null;
        }

        public SettingsApplication Application
        {
            get { return _application; }
        }
        #endregion

        #region Direcotory
        public bool OpenDirectory(string applicationName, string directory)
        {
            return Task.Run(() => OpenDirectoryAsync(applicationName, 1, directory, 0)).Result;
        }

        public async Task<bool> OpenDirectoryAsync(string applicationName, string directory)
        {
            return await OpenDirectoryAsync(applicationName, 1, directory, 0);
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
            }

            if (_application == null)
                return false;

            _directoryProvider = new DirectoryProvider(_url, _apiKey, applicationName, directory);

            _directory = await _directoryProvider.Get();

            if (_directory != null)
            {
                _settingsProvider = new SettingsProvider(_url, _apiKey, applicationName, version, directory, objectId);
                _items = await _settingsProvider.Get();
            }
            else
            {
                if (string.Equals(directory, _DEFAULT_DIR))
                    throw new SettingsException(string.Format("Failed to open application '{0}' version {1}. The target does not exist or you are not authorized to access it", applicationName, version));
                else
                    throw new SettingsException(string.Format("Failed to open directory '{0}' of application '{1}' version {2}. The target does not exist or you are not authorized to access it", directory, applicationName, version));
            }

            return _directory != null;
        }


        public bool CreateDirectory(string applicationName, string directoryName, string description)
        {
            try
            {
                return Task.Run(() => CreateDirectoryAsync(applicationName, directoryName, description)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
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

        public bool DeleteDirectory(string applicationName, string directoryName)
        {
            try
            {
                return Task.Run(() => DeleteDirectoryAsync(applicationName, directoryName)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
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

        public SettingsDirectory Directory
        {
            get { return _directory; }
        }
        #endregion

        #region Settings

        public IEnumerable<Setting> Items
        {
            get { return _items; }
        }

        public Setting[] Get()
        {
            try
            {
                return Task.Run(() => GetAsync()).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<Setting[]> GetAsync()
        {
            return await _settingsProvider.Get();
        }

        public Nullable<bool> GetBoolean(string key)
        {
            try
            {
                return Task.Run(() => GetBooleanAsync(key)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<Nullable<bool>> GetBooleanAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToBoolean(s.Value);

            return null;
        }

        public byte[] GetByteArray(string key)
        {
            try
            {
                return Task.Run(() => GetByteArrayAsync(key)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<byte[]> GetByteArrayAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return SerializationHelper.FromBase64String(s.Value);

            return null;
        }

        public Nullable<DateTime> GetDateTime(string key)
        {
            try
            {
                return Task.Run(() => GetDateTimeAsync(key)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<Nullable<DateTime>> GetDateTimeAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToDateTime(s.Value);

            return null;
        }

        public Nullable<double> GetDouble(string key)
        {
            try
            {
                return Task.Run(() => GetDoubleAsync(key)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<Nullable<double>> GetDoubleAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToDouble(s.Value);

            return null;
        }

        public Image GetImage(string key)
        {
            try
            {
                return Task.Run(() => GetImageAsync(key)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<Image> GetImageAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return SerializationHelper.ToImage(s.Value);

            return null;
        }

        public Nullable<int> GetInt(string key)
        {
            try
            {
                return Task.Run(() => GetIntAsync(key)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<Nullable<int>> GetIntAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToInt32(s.Value);

            return null;
        }

        public Setting GetKey(string key)
        {
            try
            {
                return GetKeyAsync(key).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public T GetKey<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(GetKey(key).Value);
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

            var settings = await _settingsProvider.Get(key);

            if (settings != null)
                return settings.SingleOrDefault();
            else
            {
                throw new ArgumentOutOfRangeException("key");
            }
        }

        public string GetString(string key)
        {
            try
            {
                return Task.Run(() => GetStringAsync(key)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<string> GetStringAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToString(s.Value);

            return null;
        }

        public bool Save(string key, string value)
        {
            try
            {
                return Task.Run(() => SaveAsync(key, value)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Save(string key, bool value)
        {
            try
            {
                return Task.Run(() => SaveAsync(key, value.ToString())).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Save(string key, DateTime value)
        {
            try
            {
                return Task.Run(() => SaveAsync(key, value.ToString())).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Save(string key, decimal value)
        {
            try
            {
                return Task.Run(() => SaveAsync(key, value.ToString())).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Save(string key, Image value)
        {
            try
            {
                return Task.Run(() => SaveAsync(key, SerializationHelper.ImageToString(value))).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Save(string key, byte[] value)
        {
            try
            {
                return Task.Run(() => SaveAsync(key, SerializationHelper.ToBase64String(value))).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Save(IEnumerable<Setting> settings)
        {
            try
            {
                return Task.Run(() => SaveAsync(settings)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<bool> SaveAsync(IEnumerable<Setting> settings)
        {
            return await _settingsProvider.Save(settings);
        }

        public async Task<bool> SaveAsync(Setting setting)
        {
            return await SaveAsync(new Setting[] { setting });
        }

        public async Task<bool> SaveAsync(string key, string value)
        {
            return await SaveAsync(new Setting { Key = key, Value = value });
        }
        #endregion
    }
}