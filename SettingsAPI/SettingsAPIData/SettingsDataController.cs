using SettingsAPIData.Data;
using SettingsAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class SettingsDataController : ISettingsDataController
    {
        ISettingsRepository Repository;

        public SettingsDataController(ISettingsRepository repository) 
        {
            Repository = repository;
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
                        throw new SettingsAuthorizationException(AuthorizationScope.Key, AuthorizationLevel.Write, item.Key, Repository.CurrentIdentity);
                    }
                }
                else
                {
                    if (access.AllowCreate)
                    {
                        existingOrNew = CreateDataForStore(store);
                        existingOrNew.SettingKey = item.Key;
                        existingOrNew.SettingValue = item.Value;
                        Repository.Context.Settings.Add(existingOrNew);
                    }
                    else
                    {
                        throw new SettingsAuthorizationException(AuthorizationScope.Key, AuthorizationLevel.Create, item.Key, Repository.CurrentIdentity);
                    }
                }
            }

            Repository.Context.SaveChanges();
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
                var r = Repository.Context.GetRepository(store.ApplicationName, store.Version);
                var d = Repository.Context.GetDirectory(store.ApplicationName, store.Directory);

                return Repository.Context.Settings.Where(s =>
                     s.VersionId == r.Id
                  && s.DirectoryId == d.Id
                  && (s.ObjecId == store.ObjectId || store.ObjectId == null));
            }
            else
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Directory, AuthorizationLevel.Read, store.Directory, Repository.CurrentIdentity);
            }
        } 

        private SettingData CreateDataForStore(SettingStore store)
        {
            SettingData data = new SettingData();

            var repository = Repository.Context.GetRepository(store.ApplicationName, store.Version);

            if (repository == null)
            {
                throw new SettingsStoreException("Repository does not exist.");
            }
            var directory = Repository.Context.GetDirectory(store.ApplicationName, store.Directory);

            if (directory == null)
            {
                throw new SettingsStoreException("Directory does not exist.");
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
            if (Repository.CurrentApiKey == null)
                return null;

            var dir = Repository.Context.GetDirectory(store.ApplicationName, store.Directory);

            if (Repository.IsMasterKey)
            {
                return new DirectoryAccessModel
                {
                    DirectoryId = dir.Id,
                    AllowWrite = true,
                    AllowDelete = true,
                    AllowCreate = true

                };
            }

            var accessData = Repository.Context.Access.Where(
                 d => d.DirectoryId == dir.Id
              && d.ApiKeyId == Repository.CurrentIdentity).SingleOrDefault();

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
            var repository = Repository.Context.GetRepository(store.ApplicationName, store.Version);

            var directory = Repository.Context.GetDirectory(store.ApplicationName, store.Directory);

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
