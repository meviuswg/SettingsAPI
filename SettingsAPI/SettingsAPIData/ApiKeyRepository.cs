using SettingsAPIData.Data;
using SettingsAPIData.Model;
using SettingsAPIShared;
using System;
using System.Linq;

namespace SettingsAPIData
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private SettingsDbContext context;
        private bool _dbOnline;

        public ApiKeyRepository()
        {
        }

        private SettingsDbContext Context
        {
            get
            {

                try
                {
                    context = new SettingsDbContext();
                    var test = context.Applications.Count();
                    _dbOnline = true;
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    throw new SettingsStoreException(Constants.ERROR_STORE_UNAVAILABLE, ex);
                }

                try
                {
                    return context;
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    throw new SettingsStoreException(Constants.ERROR_STORE_EXCEPTION, ex);
                }
            }
        }

        public ApiKeyModel GetKey(string apiKey)
        {
            var data = GetData(apiKey);

            if (data != null)
            {
                var model = new ApiKeyModel
                {
                    Active = data.Active,
                    AdminKey = data.AdminKey,
                    Id = data.Id,
                    Key = data.ApiKey,
                    ApplicationName = data.Application.Name
                };

                foreach (var item in data.Access)
                {
                    model.Access.Add(new ApiAccessModel
                    {
                        ApplicationName = item.Directory.Application.Name,
                        DirectoryName = item.Directory.Name,
                        AllowCreate = item.AllowCreate,
                        AllowDelete = item.AllowDelete,
                        AllowWrite = item.AllowWrite
                    });
                }

                return model;
            }

            return null;
        }

        private ApiKeyData GetData(string key)
        {
            ApiKeyData data = Context.ApiKeys.SingleOrDefault(a => a.ApiKey == key);
            return data;
        }

        public void SetUsed(string apiKey)
        {
            var data = GetData(apiKey);

            if (data != null)
            {
                data.LastUsed = DateTime.UtcNow;
                Context.SaveChanges();
            }
        }
         
    }
}