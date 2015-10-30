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
    public class ApiKeyRepository : IApiKeyRepository
    {
        private ISettingsAuthorizationProvider Auth;
        private ISettingsDatabase Store;

        public ApiKeyRepository(ISettingsDatabase store, ISettingsAuthorizationProvider provider)
        {
            Store = store;
            Auth = provider;
        }

        public IEnumerable<ApiKeyModel> GetApplicationApiKeys(string applicationName)
        {
            var data = new List<ApiKeyData>();
            List<ApiKeyModel> result = new List<ApiKeyModel>();



            if (!Auth.AllowReadApiKeys(applicationName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.ApiKey, AuthorizationLevel.Read, applicationName, Auth.CurrentIdentity.Id);
            }
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
                    if (string.Equals(a.Directory.Application.Name, applicationName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        DirectoryAccessModel access = new DirectoryAccessModel();
                        access.Create = a.AllowCreate;
                        access.Write = a.AllowWrite;
                        access.Delete = a.AllowDelete;
                        access.Directory = a.Directory.Name;
                        model.Access.Add(access);
                    }
                }

                result.Add(model);
            }


            return result;
        }

        public ApiKeyModel GetApiKey(string applicationName, string name)
        {
            if (!Auth.AllowReadApiKeys(applicationName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.ApiKey, AuthorizationLevel.Read, applicationName, Auth.CurrentIdentity.Id);
            }

            var data = GetKeyData(applicationName, name);

            if (data == null)
            {
                throw new SettingsNotFoundException("ApiKey");
            }

            var model = new ApiKeyModel
            {
                Active = data.Active,
                AdminKey = data.AdminKey,
                Key = data.ApiKey,
                ApplicationName = data.Application.Name,
                Name = data.Name
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

        private ApiKeyData GetKeyData(string applicationName, string name)
        {
            ApiKeyData data = Store.Context.ApiKeys.SingleOrDefault(a => a.Name == name && a.Application.Name == applicationName);
            return data;
        }

        private ApplicationData GetApplicationData(string applicationName)
        {
            ApplicationData data = Store.Context.Applications.SingleOrDefault(a => a.Name == applicationName);
            return data;
        }

        private IEnumerable<ApiKeyData> GetApplicationKeysData(string applicationName)
        {
            var application = Store.Context.ApiKeys.Where(a => a.Application.Name == applicationName);

            if (application == null)
                throw new SettingsNotFoundException(applicationName);

            return application;
        }

        public ApiKeyModel CreateApiKey(string applicationName, SaveApiKeyModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("No Data");
            }

            if (!Auth.AllowEditApiKeys(applicationName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.ApiKey, AuthorizationLevel.Create, applicationName, Auth.CurrentIdentity.Id);
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new SettingsStoreException("Key has no Name");
            }

            var application = GetApplicationData(applicationName);

            if (application == null)
            {
                throw new SettingsNotFoundException(applicationName);
            }

            var existingKey = GetKeyData(applicationName, model.Name);

            if (existingKey != null)
            {
                throw new SettingsDuplicateException("Key with name already exist");
            }

            var apiKeyData = new ApiKeyData();

            using (TransactionScope scope = TransactionScopeFactory.CreateReaduncommited())
            {
                apiKeyData.ApiKey = ApiKeyGenerator.Create();
                apiKeyData.ApplicationId = application.Id;
                apiKeyData.Active = true;
                apiKeyData.AdminKey = model.AdminKey;
                apiKeyData.Created = DateTime.Now;
                apiKeyData.Name = model.Name;
                Store.Context.ApiKeys.Add(apiKeyData);
                Store.Save();

                if (model.Access != null && model.Access.Count > 0)
                {
                    foreach (var item in model.Access)
                    {
                        var directiry = application.Directories.SingleOrDefault(d => d.Name == item.Directory);

                        if (directiry == null)
                        {
                            throw new SettingsNotFoundException(item.Directory);
                        }

                        DirectoryAccessData access = new DirectoryAccessData();

                        access.DirectoryId = directiry.Id;
                        access.ApiKeyId = apiKeyData.Id;
                        access.AllowWrite = item.Write;
                        access.AllowDelete = item.Delete;
                        access.AllowCreate = item.Create;

                        apiKeyData.Access.Add(access);
                    }

                    Store.Save();
                }

                scope.Complete();
            }

            return GetApiKey(applicationName, apiKeyData.Name);

        }

        public void UpdateApiKey(string applicationName, SaveApiKeyModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("ApiKey");
            }

            if (!Auth.AllowEditApiKeys(applicationName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.ApiKey, AuthorizationLevel.Create, applicationName, Auth.CurrentIdentity.Id);
            }

            var application = GetApplicationData(applicationName);

            if (application == null)
            {
                throw new SettingsNotFoundException(applicationName);
            }

            var apiKeyData = GetKeyData(applicationName, model.Key);

            if (apiKeyData == null)
            {
                throw new SettingsNotFoundException("Key");
            }

            using (TransactionScope scope = TransactionScopeFactory.CreateReaduncommited())
            {

                apiKeyData.Active = model.Active;
                apiKeyData.AdminKey = model.AdminKey;

                if (model.Access != null)
                {
                    apiKeyData.Access.Clear();
                    Store.Save();

                    foreach (var item in model.Access)
                    {
                        var directiry = application.Directories.SingleOrDefault(d => d.Name == item.Directory);

                        if (directiry == null)
                        {
                            if (application == null)
                            {
                                throw new SettingsNotFoundException(item.Directory);
                            }
                        }

                        DirectoryAccessData access = new DirectoryAccessData();

                        access.DirectoryId = directiry.Id;
                        access.ApiKeyId = apiKeyData.Id;
                        access.AllowWrite = item.Write;
                        access.AllowDelete = item.Delete;
                        access.AllowCreate = item.Create;

                        apiKeyData.Access.Add(access);
                    }
                }

                Store.Save();
                scope.Complete();
            }
        }

        public void DeleteApiKey(string applicationName, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (!Auth.AllowEditApiKeys(applicationName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.ApiKey, AuthorizationLevel.Delete, applicationName, Auth.CurrentIdentity.Id);
            }

            var application = GetApplicationData(applicationName);

            if (application == null)
            {
                throw new SettingsNotFoundException(applicationName);
            }

            var apiKeyData = GetKeyData(applicationName, name);

            if (apiKeyData == null)
            {
                throw new SettingsNotFoundException("Key");
            }

            using (TransactionScope scope = TransactionScopeFactory.CreateReaduncommited())
            {
                apiKeyData.Access.Clear();
                Store.Save();
                Store.Context.ApiKeys.Remove(apiKeyData);
                Store.Save();
                scope.Complete();
            }
        }
    }
}