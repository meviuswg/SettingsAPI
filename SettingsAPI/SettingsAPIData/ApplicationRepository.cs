using SettingsAPIData.Data;
using SettingsAPIData.Model;
using SettingsAPIData.Util;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SettingsAPIData
{
    public class ApplicationRepository : IApplicationRepository
    {
        private ISettingsStore Store;
        private ISettingsAuthorizationProvider Auth;

        public ApplicationRepository(ISettingsStore repository, ISettingsAuthorizationProvider authorizationProvider)
        {
            Store = repository;
            Auth = authorizationProvider;
        }

        #region Version

        public VersionModel GetVersion(string applicationName, int version)
        {
            var versions = GetVersions(applicationName);
            return versions.SingleOrDefault(v => v.Version == version);
        }

        public IEnumerable<VersionModel> GetVersions(string applicationName)
        {
            var application = GetApplicationsFromStore(applicationName).SingleOrDefault();

            if (application == null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_APPLICATION_UNKNOWN);
            }

            var data = GetVersionsFromStore(applicationName);

            List<VersionModel> versions = new List<VersionModel>();

            foreach (var item in data)
            {
                VersionModel version = new VersionModel();
                version.Created = item.Created;
                version.Version = item.Version;

                versions.Add(version);
            }

            return versions;
        }

        public void CreateVersion(string applicationName, int version)
        {
            if (!Auth.AllowDeleteVersion(applicationName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Version, AuthorizationLevel.Create, applicationName, Auth.CurrentIdentity.Id);
            }

            var application = GetApplicationsFromStore(applicationName).SingleOrDefault();

            if (application == null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_APPLICATION_UNKNOWN);
            }

            var data = GetVersionsFromStore(applicationName, version).SingleOrDefault();

            if (data != null)
            {
                throw new SettingsDuplicateException(Constants.ERROR_VERION_ALREADY_EXISTS);
            }

            data = new VersionData();
            data.ApplicationId = application.Id;
            data.Created = DateTime.UtcNow;
            data.Version = version;
            application.Versions.Add(data);
            Store.Save();
        }

        public void DeleteVersion(string applicationName, int version)
        {
            if (!Auth.AllowDeleteVersion(applicationName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Version, AuthorizationLevel.Delete, applicationName, Auth.CurrentIdentity.Id);
            }
            var application = GetApplicationsFromStore(applicationName).SingleOrDefault();

            if (application == null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_APPLICATION_UNKNOWN);
            }

            var data = GetVersionsFromStore(applicationName, version).SingleOrDefault();

            if (data == null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_VERION_UNKNOWN);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                var settings = Store.Context.Settings.Where(s => s.VersionId == data.Id);
                Store.Context.Settings.RemoveRange(settings);
                Store.Context.SaveChanges();
                Store.Context.Versions.Remove(data);
                Store.Context.SaveChanges();

                scope.Complete();
            }
        }

        private IEnumerable<VersionData> GetVersionsFromStore(string applicationName, int? version = null)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new SettingsStoreException("Application name not provided");
            }

            var data = (from ver in Store.Context.Versions
                        where ver.Application.Name == applicationName
                        && (ver.Version == version || true == (version == null))
                        select ver);

            return data;
        }

        #endregion Version

        #region Directory

        public void CreateDirectory(string applicationName, string directoryName, string description)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
            {
                throw new SettingsStoreException(Constants.ERROR_DIRECTORY_NO_NAME);
            }

            if (!Auth.AllowCreateDirectory(applicationName, directoryName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Directory, AuthorizationLevel.Create, directoryName, Auth.CurrentIdentity.Id);
            }

            var directory = GetDirectoriesFromStore(applicationName, directoryName).SingleOrDefault();

            var application = GetApplicationsFromStore(applicationName).SingleOrDefault();

            if (application == null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_APPLICATION_UNKNOWN);
            }

            if (directory != null)
            {
                throw new SettingsDuplicateException(Constants.ERROR_DIRECTORY_ALREADY_EXISTS);
            }

            if (directory.Name.StartsWith(Constants.SYSTEM_RESERVED_PREFIX))
            {
                throw new SettingsNotFoundException(Constants.ERROR_DIRECTORY_NAME_INVALID);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                directory = new DirectoryData();
                directory.ApplicationId = application.Id;
                directory.Name = directoryName;
                directory.Description = description;
                directory.Created = DateTime.Now;

                Store.Context.Directories.Add(directory);
                Store.Save();

                scope.Complete();
                Auth.Invalidate();
            }
        }

        public void DeleteDirectory(string applicationName, string directoryName)
        {

            var directory = GetDirectoriesFromStore(applicationName, directoryName).SingleOrDefault();

            var application = GetApplicationsFromStore(applicationName).SingleOrDefault();

            if (application == null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_APPLICATION_UNKNOWN);
            }

            if (directory != null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_DIRECTORY_UNKOWN);
            }

            if (!Auth.AllowDeleteDirectory(applicationName, directoryName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Directory, AuthorizationLevel.Delete, directoryName, Auth.CurrentIdentity.Id);
            }

            if (directory.Name.StartsWith(Constants.SYSTEM_RESERVED_PREFIX))
            {
                throw new SettingsNotFoundException(Constants.ERROR_DIRECTORY_CANNOT_DELETE);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                var settings = Store.Context.Settings.Where(s => s.DirectoryId == directory.Id);
                var access = Store.Context.Access.Where(s => s.DirectoryId == directory.Id);

                Store.Context.Settings.RemoveRange(settings);
                Store.Save();
                Store.Context.Access.RemoveRange(access);
                Store.Save();
                Store.Context.Directories.Remove(directory);
                Store.Save();

                scope.Complete();

                Auth.Invalidate();
            }
        }

        public IEnumerable<DirectoryModel> GetDirectories(string applicationName)
        {
            return GetDirectories(applicationName, null);
        }

        public DirectoryModel GetDirectory(string applicationName, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
            {
                throw new SettingsStoreException(Constants.ERROR_DIRECTORY_NO_NAME);
            }

            return GetDirectories(applicationName, directoryName).SingleOrDefault();
        }

        private IEnumerable<DirectoryModel> GetDirectories(string applicationName, string directoryName)
        {
            var data = GetDirectoriesFromStore(applicationName, directoryName);

            List<DirectoryModel> directories = new List<DirectoryModel>();

            foreach (var item in data)
            {
                DirectoryModel model = new DirectoryModel();

                model.AllowCreate = Auth.AllowCreateSetting(applicationName, item.Name);
                model.AllowDelete = Auth.AllowDeleteSetting(applicationName, item.Name);
                model.AllowWrite = Auth.AllowWriteSetting(applicationName, item.Name);

                model.Description = item.Description;
                model.Name = item.Name;
                model.Items = item.Settings.Count();

                directories.Add(model);
            }

            return directories;
        }

        private IEnumerable<DirectoryData> GetDirectoriesFromStore(string applicationName, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new SettingsStoreException(Constants.ERROR_APPLICATION_NO_NAME);
            }

            if (string.IsNullOrWhiteSpace(directoryName))
                directoryName = null;

            var data = (from dir in Store.Context.Directories
                        where dir.Access.FirstOrDefault(acc => acc.ApiKeyId == Auth.CurrentIdentity.Id) != null
                        && dir.Application.Name == applicationName
                        && (dir.Name == directoryName || true == (directoryName == null))
                        select dir);

            return data;
        }

        #endregion Directory

        #region Application

        public ApplicationModel GetApplication(string name)
        {
            return GetApplications(name).SingleOrDefault();
        }

        public IEnumerable<ApplicationModel> GetApplications()
        {
            return GetApplications(null);
        }

        private IEnumerable<ApplicationModel> GetApplications(string applicationName = null)
        {
            var data = GetApplicationsFromStore(applicationName);

            List<ApplicationModel> applicationModels = new List<ApplicationModel>();

            foreach (var item in data)
            {
                ApplicationModel model = new ApplicationModel();

                model.Name = item.Name;
                model.Description = item.Description;
                model.Versions = GetVersions(item.Name);
                model.Directories = GetDirectories(item.Name);
                model.Created = item.Created;

                model.AllowEdit = Auth.AllowCreateDirectories(item.Name);

                applicationModels.Add(model);
            }

            if (!string.IsNullOrWhiteSpace(applicationName) && applicationModels.Count == 0)
            {
                throw new SettingsNotFoundException(applicationName);
            }

            return applicationModels;
        }

        public ApplicationModel CreateApplication(string applicationName)
        {
            return CreateApplication(applicationName, string.Empty, string.Empty, string.Empty);
        }

        public ApplicationModel CreateApplication(string applicationName, string applicationDescription)
        {
            return CreateApplication(applicationName, applicationDescription, string.Empty, string.Empty);
        }

        public ApplicationModel CreateApplication(string applicationName, string applicationDescription, string directoryName, string directoryDescription)
        {
            if (!Auth.AllowCreateApplication(applicationName))
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Application, AuthorizationLevel.Create, applicationName, Auth.CurrentIdentity.Id);
            }

            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new SettingsStoreException(Constants.ERROR_APPLICATION_NO_NAME);
            }

            var application = Store.Context.Applications.FirstOrDefault(app => app.Name == applicationName);

            if (application != null)
            {
                throw new SettingsStoreException(Constants.ERROR_APPLICATION_ALREADY_EXISTS);
            }

            if (application.Name.StartsWith(Constants.SYSTEM_RESERVED_PREFIX))
            {
                throw new SettingsNotFoundException(Constants.ERROR_APPLICATION_NAME_INVALID);
            }

            application = new ApplicationData();
            var cust_directory = new DirectoryData();
            var def_directory = new DirectoryData();

            using (TransactionScope scope = new TransactionScope())
            {
                application.Name = applicationName;

                if (string.IsNullOrWhiteSpace(applicationDescription))
                {
                    applicationDescription = Constants.DEAULT_APPLICATION_DESCRIPTION;
                }

                //Create application
                application.Description = applicationDescription;
                application.Created = DateTime.UtcNow;

                Store.Context.Applications.Add(application);
                Store.Context.SaveChanges();

                //Create version 1
                VersionData version = new VersionData { Version = 1, Created = DateTime.UtcNow, ApplicationId = application.Id };
                Store.Context.Versions.Add(version);
                Store.Context.SaveChanges();
          
                //Create application default directory
                def_directory = new DirectoryData();
                def_directory.Name = Constants.DEAULT_DIRECTORY_NAME;
                def_directory.Description = Constants.DEAULT_DIRECTORY_DESCRIPTION;
                def_directory.ApplicationId = application.Id;
                def_directory.Created = DateTime.UtcNow;
                Store.Context.Directories.Add(def_directory);

                //Create custom first directory, if provided.
                if (!string.IsNullOrWhiteSpace(directoryName))
                {
                    cust_directory = new DirectoryData();
                    cust_directory.Name = directoryName;
                    cust_directory.Description = directoryDescription;
                    cust_directory.ApplicationId = application.Id;
                    cust_directory.Created = DateTime.UtcNow;
                    Store.Context.Directories.Add(cust_directory);
                }

                Store.Context.SaveChanges();

                //Create default api key for applicaiton, a trigger maintains access for the master apikey to the application directories.
                ApiKeyData apiKey = new ApiKeyData { ApiKey = ApiKeyGenerator.Create(), Application = application, Active = true, AdminKey = true, Created = DateTime.UtcNow };

                Store.Context.ApiKeys.Add(apiKey);
                Store.Context.SaveChanges();

                //Set access right to default directory.
                DirectoryAccessData access = new DirectoryAccessData();
                access.AllowWrite = true;
                access.AllowDelete = true;
                access.AllowCreate = true;
                access.ApiKey = apiKey;
                access.Directory = def_directory;
                Store.Context.Access.Add(access);

                //Set access right to custom directory. 
                if (cust_directory != null)
                {
                    access = new DirectoryAccessData();
                    access.AllowWrite = true;
                    access.AllowDelete = true;
                    access.AllowCreate = true;
                    access.ApiKey = apiKey;
                    access.Directory = cust_directory;
                    Store.Context.Access.Add(access);
                }

                Store.Save();
                scope.Complete();
                Auth.Invalidate();
            }
             
            //reload the enities the reflect the master key access created by the trigger.
            if (cust_directory != null)
                Store.Context.Entry<DirectoryData>(cust_directory).Collection("Access").Load();

            if (cust_directory != null)
                Store.Context.Entry<DirectoryData>(def_directory).Collection("Access").Load();

            return GetApplication(applicationName);
        }

        private IEnumerable<ApplicationData> GetApplicationsFromStore(string applicationName)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
                applicationName = null;

            if (Auth.CurrentIdentity == null)
                throw new SettingsAuthorizationException(AuthorizationScope.Application, AuthorizationLevel.Read, "ApiKey", 0);

            var applications = (from app in Store.Context.Applications
                                where (app.Name == applicationName || applicationName == null) &&
                                (app.ApiKeys.FirstOrDefault(a => a.Id == Auth.CurrentIdentity.Id) != null
                                || true == Auth.IsMasterKey)
                                select app);

            return applications;
        }

        public void DeleteApplication(string applicationName)
        {
            if (Auth.AllowDeleteApplication(applicationName))
            {
                var application = Store.Context.Applications.FirstOrDefault(a => a.Name == applicationName);

                if (application != null)
                {
                    if (applicationName.StartsWith(Constants.SYSTEM_RESERVED_PREFIX))
                    {
                        throw new SettingsStoreException(Constants.ERROR_APPLICATION_CANNOT_DELETED);
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        var directories = Store.Context.Directories.Where(d => d.ApplicationId == application.Id);

                        var settings = Store.Context.Settings.Where(s => s.DirectoryId == application.Id);
                        Store.Context.Settings.RemoveRange(settings);

                        Store.Context.SaveChanges();

                        foreach (var item in directories)
                        {
                            DeleteDirectory(application.Name, item.Name);
                        }

                        var repositories = Store.Context.Versions.Where(s => s.Application.Id == application.Id);
                        Store.Context.Versions.RemoveRange(repositories);

                        Store.Context.SaveChanges();

                        var keys = Store.Context.ApiKeys.Where(k => k.Application.Id == application.Id);
                        Store.Context.ApiKeys.RemoveRange(keys);
                        Store.Context.SaveChanges();

                        Store.Context.Applications.Remove(application);
                        Store.Context.SaveChanges();

                        scope.Complete();

                        Auth.Invalidate();
                    }
                }
                else
                {
                    throw new SettingsNotFoundException(applicationName);
                }
            }
            else
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Application, AuthorizationLevel.Delete, applicationName, Auth.CurrentIdentity.Id);
            }
        }

        #endregion Application 
    }
}