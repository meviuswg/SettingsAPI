using SettingsAPIData.Data;
using SettingsAPIData.Model;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SettingsAPIData
{
    public class SettingsRepository : ISettingsRepository
    {
        private ISettingsStore Store;
        private ISettingsAuthorizationProvider Auth;

        public SettingsRepository(ISettingsStore store, ISettingsAuthorizationProvider provider)
        {
            Store = store;
            Auth = provider;
        }

        public void SaveSetting(SettingStore store, SettingModel setting)
        {
            SaveSettings(store, new[] { setting });
        }

        public void SaveSettings(SettingStore store, IEnumerable<SettingModel> settings)
        {
            var currentSettings = GetSettingsFromStore(store);
            var access = GetAccessData(store);

            foreach (var item in settings)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.Key))
                    continue;

                var existingOrNew = currentSettings.SingleOrDefault(s => s.Equals(item));

                if (existingOrNew != null)
                {
                    if (access.AllowWrite)
                    {
                        existingOrNew.SettingValue = item.Value;
                        existingOrNew.Modified = DateTime.UtcNow;
                    }
                    else
                    {
                        throw new SettingsAuthorizationException(AuthorizationScope.Key, AuthorizationLevel.Write, item.Key, Auth.CurrentIdentity.Id);
                    }
                }
                else
                {
                    if (access.AllowCreate)
                    {
                        existingOrNew = CreateDataForStore(store);
                        existingOrNew.SettingKey = item.Key;
                        existingOrNew.SettingValue = item.Value;
                        Store.Context.Settings.Add(existingOrNew);
                    }
                    else
                    {
                        throw new SettingsAuthorizationException(AuthorizationScope.Key, AuthorizationLevel.Create, item.Key, Auth.CurrentIdentity.Id);
                    }
                }
            }

            Store.Save();
        }

        public SettingModel GetSetting(SettingStore store, string settingKey)
        {
            return (from setting in GetSettingsFromStore(store)
                    where setting.SettingKey == settingKey

                    select new SettingModel
                    {
                        Key = setting.SettingKey,
                        Value = setting.SettingValue,
                        ObjectId = setting.ObjecId
                    }).SingleOrDefault();
        }

        public IEnumerable<SettingModel> GetSettings(SettingStore store)
        {
            return (from setting in GetSettingsFromStore(store)
                    select new SettingModel
                    {
                        Key = setting.SettingKey,
                        Value = setting.SettingValue,
                        ObjectId = setting.ObjecId
                    });
        }

        private IEnumerable<SettingData> GetSettingsFromStore(SettingStore store)
        {
            var access = GetAccessData(store);

            if (access != null)
            {
                var r = Store.GetVersion(store.ApplicationName, store.Version);
                var d = Store.GetDirectory(store.ApplicationName, store.Directory);

                return Store.Context.Settings.Where(s =>
                     s.VersionId == r.Id
                  && s.DirectoryId == d.Id
                  && (s.ObjecId == store.ObjectId || store.ObjectId == null));
            }
            else
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Directory, AuthorizationLevel.Read, store.Directory, Auth.CurrentIdentity.Id);
            }
        }

        private SettingData CreateDataForStore(SettingStore store)
        {
            SettingData data = new SettingData();

            var repository = Store.GetVersion(store.ApplicationName, store.Version);

            if (repository == null)
            {
                throw new SettingsStoreException(Constants.ERROR_VERION_UNKNOWN);
            }
            var directory = Store.GetDirectory(store.ApplicationName, store.Directory);

            if (directory == null)
            {
                throw new SettingsStoreException(Constants.ERROR_DIRECTORY_UNKOWN);
            }

            data.Directory = directory;
            data.Repository = repository;
            data.ObjecId = store.ObjectId ?? 0;

            return data;
        }

        public bool AllowRead(SettingStore store)
        {
            DirectoryAccessModel accessData = GetAccessData(store);

            if (accessData != null)
                return true;

            return false;
        }

        public bool AllowWrite(SettingStore store)
        {
            DirectoryAccessModel accessData = GetAccessData(store);

            if (accessData != null && (accessData.AllowWrite || accessData.AllowCreate))
                return true;

            return false;
        }

        public bool AllowCreate(SettingStore store)
        {
            DirectoryAccessModel accessData = GetAccessData(store);

            if (accessData != null && accessData.AllowCreate)
                return true;

            return false;
        }

        public bool AllowDelete(SettingStore store)
        {
            DirectoryAccessModel accessData = GetAccessData(store);

            if (accessData != null && accessData.AllowDelete)
                return true;

            return false;
        }

        private DirectoryAccessModel GetAccessData(SettingStore store)
        {
            if (Auth.CurrentApiKey == null)
                return null;

            var dir = Store.GetDirectory(store.ApplicationName, store.Directory);

            if (Auth.IsMasterKey)
            {
                return new DirectoryAccessModel
                {
                    DirectoryId = dir.Id,
                    AllowWrite = true,
                    AllowDelete = true,
                    AllowCreate = true
                };
            }

            var accessData = Store.Context.Access.Where(
                 d => d.DirectoryId == dir.Id
              && d.ApiKeyId == Auth.CurrentIdentity.Id).SingleOrDefault();

            if (accessData != null && accessData.ApiKey.Active)
            {
                return new DirectoryAccessModel
                {
                    DirectoryId = dir.Id,
                    AllowWrite = accessData.AllowWrite,
                    AllowDelete = accessData.AllowDelete,
                    AllowCreate = accessData.AllowCreate
                };
            }

            return null;
        }

        public bool Exists(SettingStore store)
        {
            var repository = Store.GetVersion(store.ApplicationName, store.Version);

            var directory = Store.GetDirectory(store.ApplicationName, store.Directory);

            return (repository != null && directory != null);
        }

        public bool Exists(SettingStore store, string settingKey)
        {
            if (Exists(store))
            {
                return GetSetting(store, settingKey) != null;
            }

            return false;
        }
    }
}