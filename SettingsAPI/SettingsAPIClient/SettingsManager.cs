using Newtonsoft.Json;
using SettingsAPIClient.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingsManager
    {
        private SettingProvider _internalProvider;

        public SettingsManager(string url, string apiKey, SettingsStore store)
        {
            _internalProvider = new SettingProvider(url, apiKey, store);
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
            return await _internalProvider.ExecuteReadFromRemoteStore();
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

            var settings = await _internalProvider.ExecuteReadFromRemoteStore(key);

            if (settings != null)
                return settings.SingleOrDefault();
            else
            {
                throw new ArgumentOutOfRangeException("key");
            }
        }

        public string GetString(string key)
        {
            return Task.Run(() => GetStringAsync(key)).Result;
        }

        public async Task<string> GetStringAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToString(s.Value);

            return null;
        }

        public Nullable<bool> GetBoolean(string key)
        {
            return Task.Run(() => GetBooleanAsync(key)).Result;
        }

        public async Task<Nullable<bool>> GetBooleanAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToBoolean(s.Value);

            return null;
        }

        public Nullable<int> GetInt(string key)
        {
            return Task.Run(() => GetIntAsync(key)).Result;
        }

        public async Task<Nullable<int>> GetIntAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToInt32(s.Value);

            return null;
        }

        public Nullable<double> GetDouble(string key)
        {
            return Task.Run(() => GetDoubleAsync(key)).Result;
        }

        public async Task<Nullable<double>> GetDoubleAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToDouble(s.Value);

            return null;
        }

        public Nullable<DateTime> GetDateTime(string key)
        {
            return Task.Run(() => GetDateTimeAsync(key)).Result;
        }

        public async Task<Nullable<DateTime>> GetDateTimeAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return Convert.ToDateTime(s.Value);

            return null;
        }

        public byte[] GetByteArray(string key)
        {
            return Task.Run(() => GetByteArrayAsync(key)).Result;
        }

        public async Task<byte[]> GetByteArrayAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return SerializationHelper.FromBase64String(s.Value);

            return null;
        }

        public Image GetImage(string key)
        {
            return Task.Run(() => GetImageAsync(key)).Result;
        }

        public async Task<Image> GetImageAsync(string key)
        {
            var s = await GetKeyAsync(key);

            if (s != null && s.Value != null)
                return SerializationHelper.ToImage(s.Value);

            return null;
        }

        public async Task<bool> SaveAsync(Setting[] settings)
        {
            return await _internalProvider.ExecuteSaveToRemoteStore(settings);
        }

        public async Task<bool> SaveAsync(Setting setting)
        {
            return await SaveAsync(new Setting[] { setting });
        }

        public async Task<bool> SaveAsync(string key, string value)
        {
            return await SaveAsync(new Setting { Key = key, Value = value });

        }

        public bool Save(string key, string value)
        {
            return Task.Run(() => SaveAsync(key, value)).Result;
        }

        public bool Save(string key, bool value)
        {
            return Task.Run(() => SaveAsync(key, value.ToString())).Result;
        }

        public bool Save(string key, DateTime value)
        {
            return Task.Run(() => SaveAsync(key, value.ToString())).Result;
        }

        public bool Save(string key, decimal value)
        {
            return Task.Run(() => SaveAsync(key, value.ToString())).Result;
        }

        public bool Save(string key, double value)
        {
            return Task.Run(() => SaveAsync(key, value.ToString())).Result;
        }

        public bool Save(string key, Image value)
        {
            return Task.Run(() => SaveAsync(key, SerializationHelper.ImageToString(value))).Result;
        }

        public bool Save(string key, byte[] value)
        {
            return Task.Run(() => SaveAsync(key, SerializationHelper.ToBase64String(value))).Result;
        }


    }
}
