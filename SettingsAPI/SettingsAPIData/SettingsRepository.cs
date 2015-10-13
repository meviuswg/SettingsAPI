using SettingsAPIData.Data;
using SettingsAPIData.Model;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SettingsAPIData
{
    public class SettingsRepository : ISettingsRepository
    {
        private ISettingsAuthorizationProvider Auth;
        private ISettingsStore Store;

        public SettingsRepository(ISettingsStore store, ISettingsAuthorizationProvider provider)
        {
            Store = store;
            Auth = provider;
        }

        public SettingModel GetSetting(SettingStore store, string settingKey)
        {
            //will authenticate
            return (from setting in GetSettingsFromStore(store)
                    where setting.SettingKey == settingKey

                    select new SettingModel
                    {
                        Key = setting.SettingKey,
                        Value = setting.SettingValue
                    }).SingleOrDefault();
        }

        public IEnumerable<SettingModel> GetSettings(SettingStore store)
        {
            //will authenticate
            return (from setting in GetSettingsFromStore(store)
                    select new SettingModel
                    {
                        Key = setting.SettingKey,
                        Value = setting.SettingValue
                    });
        }

        public void SaveSetting(SettingStore store, SettingModel setting)
        {
            SaveSettings(store, new[] { setting });
        }

        public void SaveSettings(SettingStore store, IEnumerable<SettingModel> settings)
        {
            var currentSettings = GetSettingsFromStore(store);

            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var item in settings)
                {
                    if (item == null || string.IsNullOrWhiteSpace(item.Key))
                    {
                        throw new SettingsStoreException(Constants.ERROR_SETTING_NO_KEY);
                    }

                    var existingOrNew = currentSettings.SingleOrDefault(s => s.Equals(item));

                    if (existingOrNew != null)
                    {
                        if (Auth.AllowWriteSetting(store.ApplicationName, store.DirectoryName))
                        {
                            existingOrNew.SettingValue = item.Value;
                            existingOrNew.Modified = DateTime.UtcNow;
                        }
                        else
                        {
                            throw new SettingsAuthorizationException(AuthorizationScope.Setting, AuthorizationLevel.Write, store.DirectoryName, Auth.CurrentIdentity.Id);
                        }
                    }
                    else
                    {
                        if (Auth.AllowCreateSetting(store.ApplicationName, store.DirectoryName))
                        {
                            existingOrNew = CreateDataForStore(store);
                            existingOrNew.SettingKey = item.Key;
                            existingOrNew.SettingValue = item.Value;
                            Store.Context.Settings.Add(existingOrNew);
                        }
                        else
                        {
                            throw new SettingsAuthorizationException(AuthorizationScope.Setting, AuthorizationLevel.Create, store.DirectoryName, Auth.CurrentIdentity.Id);
                        }
                    }
                }

                Store.Save();
                scope.Complete();
            }
        }

        private SettingData CreateDataForStore(SettingStore store)
        {
            //Must be authenticated
            SettingData data = new SettingData();

            var repository = Store.GetVersion(store.ApplicationName, store.Version);

            if (repository == null)
            {
                throw new SettingsStoreException(Constants.ERROR_VERION_UNKNOWN);
            }
            var directory = Store.GetDirectory(store.ApplicationName, store.DirectoryName);

            if (directory == null)
            {
                throw new SettingsStoreException(Constants.ERROR_DIRECTORY_UNKOWN);
            }

            data.Directory = directory;
            data.Repository = repository;
            data.ObjecId = store.ObjectId ?? 0;

            return data;
        }

        private IEnumerable<SettingData> GetSettingsFromStore(SettingStore store)
        {
            if (!Auth.AllowReadDirectory(store.ApplicationName, store.DirectoryName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Directory, AuthorizationLevel.Read, store.DirectoryName, Auth.CurrentIdentity.Id);
            }
            var version = Store.GetVersion(store.ApplicationName, store.Version);

            if (version == null)
            {
                throw new SettingsNotFoundException(store.Version.ToString());
            }

            var directory = Store.GetDirectory(store.ApplicationName, store.DirectoryName);

            if (version == null)
            {
                throw new SettingsNotFoundException(store.DirectoryName);
            }

            return Store.Context.Settings.Where(s =>
                 s.VersionId == version.Id
              && s.DirectoryId == directory.Id
              && (s.ObjecId == store.ObjectId || store.ObjectId == null));
        }
    }
}