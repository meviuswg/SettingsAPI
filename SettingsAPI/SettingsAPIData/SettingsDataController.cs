using SettingsAPIData.Model;
using SettingsAPIData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class SettingsDataController : IDisposable, ISettingsDataController
    {
        SettingsDbContext context;
        private IApiKey keyProvider;


        public SettingsDataController(SettingsDbContext context, IApiKey apiKeyProvider)
        {
            this.context = context;
            this.keyProvider = apiKeyProvider;
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
                        throw new SettingsAuthorizationException(AuthorizationScope.Key, AuthorizationLevel.Write, item.Key, CurrentIdentity);
                    }
                }
                else
                {
                    if (access.AllowCreate)
                    {
                        existingOrNew = CreateDataForStore(store);
                        existingOrNew.SettingKey = item.Key;
                        existingOrNew.SettingValue = item.Value;
                        context.Settings.Add(existingOrNew);
                    }
                    else
                    {
                        throw new SettingsAuthorizationException(AuthorizationScope.Key, AuthorizationLevel.Create, item.Key, CurrentIdentity);
                    }
                }
            }

            context.SaveChanges();
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
                var r = context.GetRepository(store.ApplicationName, store.Version);
                var d = context.GetDirectory(store.ApplicationName, store.Directory);

                return context.Settings.Where(s =>
                     s.RepositoryId == r.Id
                  && s.DirectoryId == d.Id
                  && (s.ObjecId == store.ObjectId || store.ObjectId == null));
            }
            else
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Directory, AuthorizationLevel.Read, store.Directory, CurrentIdentity);
            }
        }

        private ApiKeyData CurrentApiKey
        {
            get
            {
                return context.ApiKeys.SingleOrDefault(a => a.ApiKey == keyProvider.Key);
            }
        }

        private int CurrentIdentity
        {
            get
            {
                if (CurrentApiKey != null)
                    return CurrentApiKey.Id;

                return -1;
            }
        }

        private SettingData CreateDataForStore(SettingStore store)
        {
            SettingData data = new SettingData();

            var repository = context.GetRepository(store.ApplicationName, store.Version);

            if (repository == null)
            {
                throw new SettingsStoreException("Repository does not exist.");
            }
            var directory = context.GetDirectory(store.ApplicationName, store.Directory);

            if (directory == null)
            {
                throw new SettingsStoreException("Directory does not exist.");
            }

            data.Directory = directory;
            data.Repository = repository;
            data.ObjecId = store.ObjectId ?? 0;

            return data;

        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
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
            var dir = context.GetDirectory(store.ApplicationName, store.Directory);

            if (IsMasterKey)
            {
                return new DirectoryAccessModel
                {
                    DirectoryId = dir.Id,
                    AllowWrite = true,
                    AllowDelete = true,
                    AllowCreate = true

                };
            }

            var accessData = context.Access.Where(
                 d => d.DirectoryId == dir.Id                
              && d.ApiKeyId == CurrentIdentity).SingleOrDefault();      

            if (accessData != null && accessData.ApiKey.Active)
            {
                return new DirectoryAccessModel
                {
                    DirectoryId = dir.Id,
                    AllowWrite =  accessData.AllowWrite,
                    AllowDelete =   accessData.AllowDelete,
                    AllowCreate =  accessData.AllowCreate

                };
            }
             
            return null;
        }

        public bool Exists(SettingStore store)
        {
            var repository = context.GetRepository(store.ApplicationName, store.Version);

            var directory = context.GetDirectory(store.ApplicationName, store.Directory);

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

        private bool IsMasterKey
        {
            get { return keyProvider.Key == Constants.MASTER_API_KEY; }
        }
    }
}
