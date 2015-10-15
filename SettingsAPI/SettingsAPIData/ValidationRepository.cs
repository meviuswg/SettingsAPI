using SettingsAPIData.Data;
using SettingsAPIData.Model;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SettingsAPIData
{
    public class ValidationRepository : IValidationRepository
    {
        private SettingsDbContext context;

        public ValidationRepository()
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
                    Id = data.Id,
                    Active = data.Active,
                    AdminKey = data.AdminKey, 
                    Key = data.ApiKey,
                    ApplicationName = data.Application.Name
                };

                foreach (var item in data.Access)
                {
                    model.Access.Add(new DirectoryAccessModel
                    {
                        Application = item.Directory.Application.Name,
                        Directory = item.Directory.Name,
                        Create = item.AllowCreate,
                        Delete = item.AllowDelete,
                        Write = item.AllowWrite
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