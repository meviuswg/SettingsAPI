﻿using SettingsAPIData.Data;
using SettingsAPIData.Model;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private SettingsDbContext context;
        private bool _dbOnline;

        public ApiKeyRepository(SettingsDbContext context)
        {
            this.context = context;
        }

        private SettingsDbContext Context
        {
            get
            {

                if (!_dbOnline)
                {
                    try
                    {
                        var test = context.Applications.Count();
                        _dbOnline = true;
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(ex);
                        throw new SettingsStoreException(Constants.ERROR_STORE_UNAVAILABLE);
                    }

                }
                try
                {
                    return context;

                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    throw new SettingsStoreException(Constants.ERROR_STORE_EXCEPTION);
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
                    Key = data.ApiKey 
                   
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
            return Context.ApiKeys.SingleOrDefault(a => a.ApiKey == key);
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
