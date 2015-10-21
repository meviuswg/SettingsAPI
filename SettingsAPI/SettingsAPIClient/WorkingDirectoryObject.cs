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
    public class WorkingDirectoryObject
    {
        private int _currentObjectId;
        private SettingsDirectory _directory;
        private Dictionary<string, Setting> _items;
        private SettingsProvider _provider;

        public WorkingDirectoryObject(SettingsDirectory directory, string applicationName, int version, int objectId, string url, string apiKey)
        {
            this._directory = directory;
            this._currentObjectId = objectId;
            this._provider = new SettingsProvider(url, apiKey, applicationName, version, directory.Name);
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

        public bool ExplicitlySave { get; set; }

        /// <summary>
        /// Current settings object Id. The default objectId is 0. All settings are read and writting against this objectId.
        /// </summary>
        public int ObjectID
        {
            get
            {
                return _currentObjectId;
            }

            set
            {
                if (_currentObjectId >= -1 && _currentObjectId != value)
                {
                    _currentObjectId = value;

                    Task.Run(() => Reload()).Wait();
                }
            }
        }

        public bool UseCache { get; set; }

        #region Settings

        public IEnumerable<Setting> Items
        {
            get { return _items.Values.ToArray(); }
        }

        public string Name { get { return _directory.Name; } }

        public string Description { get { return _directory.Description; } }

        public async Task<Setting[]> GetAsync()
        {
            return await _provider.Get();
        }

        public async Task<Nullable<bool>> GetBooleanAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s.Value != null)
                return Convert.ToBoolean(s.Value);

            return null;
        }

        public async Task<byte[]> GetByteArrayAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s.Value != null)
                return SerializationHelper.FromBase64String(s.Value);

            return null;
        }

        public async Task<Nullable<DateTime>> GetDateTimeAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s.Value != null)
                return Convert.ToDateTime(s.Value);

            return null;
        }

        public async Task<Nullable<double>> GetDoubleAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s.Value != null)
                return Convert.ToDouble(s.Value);

            return null;
        }

        public async Task<Image> GetImageAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s.Value != null)
                return SerializationHelper.ToImage(s.Value);

            return null;
        }

        public async Task<Nullable<int>> GetIntAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s.Value != null)
                return Convert.ToInt32(s.Value);

            return null;
        }

        public async Task<T> GetKeyAsync<T>(string key)
        {
            var s = await GetKeyAsync(key);

            if (s.Value != null)
                return JsonConvert.DeserializeObject<T>(s.Value);

            return default(T);
        }

        public async Task<Setting> GetKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            if (await Exists(key))
            {
                if (UseCache)
                {
                    return _items[string.Format("{0}{1}", workingId, key).ToLower()];
                }
                else
                {
                    return await _provider.Get(workingId, key);
                }
            }
            else
            {
                var setting = await _provider.Get(workingId, key);

                if (setting == null)
                {
                    throw new SettingNotFoundException(key);
                }
                else
                {
                    _items[setting.Id] = setting;
                    return setting;
                }
            }
        }

        public async Task<string> GetStringAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s.Value != null)
                return Convert.ToString(s.Value);

            return null;
        }

        public async Task<bool> DeleteAsync(int objectId, string key)
        {
            return await _provider.Delete(objectId, key);
        }

        /// <summary>
        /// Reloads all the settings of the current directory.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Reload()
        {
            Setting[] settings = null;

            if (_currentObjectId == -1)
            {
                settings = await _provider.Get();
            }
            else
            {
                settings = await _provider.Get(workingId);
            }

            _items = new Dictionary<string, Setting>();

            SetInternalItemsCollection(settings);

            return true;
        }

        public async Task<bool> SaveNullAsync(string key, ValueDataType type)
        {
            return await SaveAsync(key, null, type);
        }

        public async Task<bool> SaveAsync(string key, bool value)
        {
            return await SaveAsync(key, value.ToString(), ValueDataType.Bool);
        }

        public async Task<bool> SaveAsync(string key, DateTime value)
        {
            return await SaveAsync(key, value.ToString(), ValueDataType.DateTime);
        }

        public async Task<bool> SaveAsync(string key, decimal value)
        {
            return await SaveAsync(key, value.ToString(), ValueDataType.Decimal);
        }

        public async Task<bool> SaveAsync(string key, Image value)
        {
            return await SaveAsync(key, SerializationHelper.ImageToString(value), ValueDataType.Image);
        }

        public async Task<bool> SaveAsync(string key, byte[] value)
        {
            return await SaveAsync(key, SerializationHelper.ToBase64String(value), ValueDataType.ByteArray);
        }

        public async Task<bool> SaveAsync(IEnumerable<Setting> settings)
        {
            if (ExplicitlySave)
            {
                SetInternalItemsCollection(settings);
                return true;
            }

            bool settingsSaved = await _provider.Save(settings);

            if (settingsSaved)
            {
                SetInternalItemsCollection(settings);
            }

            return settingsSaved;
        }

        public async Task<bool> SaveAsync(Setting setting)
        {
            return await SaveAsync(new Setting[] { setting });
        }

        public async Task<bool> SaveAsync(string key, string value)
        {
            return await SaveAsync(new Setting { ObjectId = workingId, Key = key, Value = value, ValueType = ValueDataType.String });
        }

        public async Task<bool> SaveAsync(string key, string value, ValueDataType dataType)
        {
            return await SaveAsync(new Setting { Key = key, Value = value, ValueType = dataType });
        }

        /// <summary>
        /// Saves all items to the remote store
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
            return await _provider.Save(_items.Values.ToList());
        }

        public async Task<bool> Exists(int objectId, string key)
        {
            if (UseCache)
            {
                return _items.ContainsKey(string.Format("{0}{1}", objectId, key).ToLower());
            }
            else
            {
                return await _provider.Exists(objectId, key);
            }
        }

        public async Task<bool> Exists(string key)
        {
            return await Exists(workingId, key);
        }

        private void SetInternalItemsCollection(IEnumerable<Setting> settings)
        {
            foreach (var item in settings)
            {
                _items[item.Id] = item;
            }
        }

        private int workingId
        {
            get
            {
                return _currentObjectId > 0 ? _currentObjectId : 0;
            }
        }
        #endregion Settings
    }
}