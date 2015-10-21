using SettingsAPIRepository.Data;
using SettingsAPIRepository.Model;
using SettingsAPIRepository.Util;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SettingsAPIRepository
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

        public SettingModel GetSetting(SettingStore store, string settingKey, int objectId)
        {
            //will authenticate
            var value = (from setting in GetSettingsFromStore(store)
                         where (setting.SettingKey == settingKey)
                         &&  setting.ObjecId == objectId
                         select DataToModel(setting)).SingleOrDefault();

            if (value == null)
            {
                throw new SettingsNotFoundException(settingKey);
            }

            return value;
        }
 

        private SettingModel DataToModel(SettingData data)
        {
            return new SettingModel
            {
                TypeInfo = data.SettingTypeInfo,
                Created = data.Created,
                Info = data.SettingInfo,
                ObjectId = data.ObjecId,
                Modified = data.Modified,
                Key = data.SettingKey,
                Value = data.SettingValue
            };

        }

        public IEnumerable<SettingModel> GetSettings(SettingStore store, int objectId)
        {
            //will authenticate
            return (from setting in GetSettingsFromStore(store)
                    where setting.ObjecId == objectId
                    select DataToModel(setting));
        }

        public IEnumerable<SettingModel> GetSettings(SettingStore store)
        {
            //will authenticate
            return (from setting in GetSettingsFromStore(store)
                    select DataToModel(setting));
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
                        if (Auth.AllowriteSetting(store.ApplicationName, store.DirectoryName))
                        {
                            existingOrNew.SettingInfo = item.Info;
                            existingOrNew.SettingTypeInfo = item.TypeInfo;
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
                            if(!NameValidator.ValidateKey(item.Key))
                            {
                                throw new SettingsStoreException(Constants.ERROR_SETTING_INVALID_KEY);
                            }
                            existingOrNew = CreateDataForStore(store);
                            existingOrNew.SettingKey = item.Key.Trim().Replace("  ", " ");
                            existingOrNew.SettingValue = item.Value;
                            existingOrNew.SettingInfo = item.Info;
                            existingOrNew.SettingTypeInfo = item.TypeInfo;
                            existingOrNew.Created = DateTime.Now;
                            existingOrNew.ObjecId = item.ObjectId;
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

            var version = Store.GetVersion(store.ApplicationName, store.Version);

            if (version == null)
            {
                throw new SettingsStoreException(Constants.ERROR_VERION_UNKNOWN);
            }
            var directory = Store.GetDirectory(store.ApplicationName, store.DirectoryName);

            if (directory == null)
            {
                throw new SettingsStoreException(Constants.ERROR_DIRECTORY_UNKOWN);
            }

            data.DirectoryId = directory.Id;
            data.VersionId = version.Id;
            data.ObjecId = 0;

            return data;
        }

        private IEnumerable<SettingData> GetSettingsFromStore(SettingStore store)
        {
            if (store == null || string.IsNullOrWhiteSpace(store.ApplicationName) || string.IsNullOrWhiteSpace(store.DirectoryName))
            {
                throw new SettingsStoreException("Invalid path");
            }

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
              && s.DirectoryId == directory.Id);
        }
    }
}