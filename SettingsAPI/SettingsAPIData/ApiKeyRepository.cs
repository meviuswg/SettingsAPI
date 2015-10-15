using SettingsAPIData.Data;
using SettingsAPIData.Model;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SettingsAPIData
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private ISettingsAuthorizationProvider Auth;
        private ISettingsStore Store;

        public ApiKeyRepository(ISettingsStore store, ISettingsAuthorizationProvider provider)
        {
            Store = store;
            Auth = provider;
        }


        public IEnumerable<ApiKeyModel> GetApplicationApiKeys(string applicationName)
        {
           var  data = new List<ApiKeyData>();
            List<ApiKeyModel> result = new List<ApiKeyModel>();


            if (Auth.AllowReadApiKeys(applicationName))
            {
                var registerdKeys = GetApplicationKeysData(applicationName);

                foreach (var item in registerdKeys)
                {
                    ApiKeyModel model = new ApiKeyModel();

                    model.Active = item.Active;
                    model.AdminKey = item.AdminKey;
                    model.ApplicationName = item.Application.Name;
                    model.Key = item.ApiKey;
                    model.LastUsed = item.LastUsed;

                    model.Access = new List<DirectoryAccessModel>();

                    foreach (var a in item.Access)
                    {
                        DirectoryAccessModel access = new DirectoryAccessModel();
                        access.Create = a.AllowCreate;
                        access.Write = a.AllowWrite;
                        access.Delete = a.AllowDelete;
                        access.Directory = a.Directory.Name;
                        access.Application = a.Directory.Application.Name;
                        model.Access.Add(access);

                        result.Add(model);
                    }
                }
            }

            return result;
        }


        public ApiKeyModel GetKey(string apiKey)
        {
            var data = GetKeyData(apiKey);

            if (data != null)
            {
                var model = new ApiKeyModel
                {
                    Active = data.Active,
                    AdminKey = data.AdminKey,
                    Key = data.ApiKey,
                    ApplicationName = data.Application.Name
                };
                
                model.Access = new List<DirectoryAccessModel>();
                foreach (var item in data.Access)
                {
                    model.Access.Add(new DirectoryAccessModel
                    {
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

      

        private ApiKeyData GetKeyData(string key)
        {
            ApiKeyData data = Store.Context.ApiKeys.SingleOrDefault(a => a.ApiKey == key);
            return data;
        }

        private IEnumerable<ApiKeyData> GetApplicationKeysData(string applicationName)
        {
            var application = Store.Context.ApiKeys.Where(a => ( a.Application.Name == applicationName && a.Id == Auth.CurrentIdentity.Id) ||(a.Application.Name  == applicationName  && (true == Auth.IsMasterKey)));

            if (application == null)
                throw new SettingsNotFoundException(applicationName);

            return application;
        }

       
    }
}