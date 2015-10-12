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
        private ISettingsStore Repository;
        private ISettingsAuthorizationProvider Auth;

        public ApplicationRepository(ISettingsStore repository, ISettingsAuthorizationProvider authorizationProvider)
        {
            Repository = repository;
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
            if (Auth.AllowCreateVersion(applicationName))
            {
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
                Repository.Save();
            }
            else
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Version, AuthorizationLevel.Create, applicationName, Auth.CurrentIdentity.Id);
            }
        }

        public void DeleteVersion(string applicationName, int version)
        {
            if (Auth.AllowDeleteVersion(applicationName))
            {
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
                    var settings = Repository.Context.Settings.Where(s => s.VersionId == data.Id);
                    Repository.Context.Settings.RemoveRange(settings);
                    Repository.Context.SaveChanges();

                    Repository.Context.Versions.Remove(data);
                    Repository.Context.SaveChanges();

                    scope.Complete();
                }
            }
            else
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Version, AuthorizationLevel.Create, applicationName, Auth.CurrentIdentity.Id);
            }
        }

        private IEnumerable<VersionData> GetVersionsFromStore(string applicationName, int? version = null)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new SettingsStoreException("Application name not provided");
            }

            var data = (from ver in Repository.Context.Versions
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

            using (TransactionScope scope = new TransactionScope())
            {
                directory = new DirectoryData();
                directory.ApplicationId = application.Id;
                directory.Name = directoryName;
                directory.Description = description;
                directory.Created = DateTime.Now;

                Repository.Context.Directories.Add(directory);
                Repository.Context.SaveChanges();
            }
        }

        public void DeleteDirectory(string applicationName, string directoryName)
        {
            var directory = GetDirectoriesFromStore(applicationName, directoryName).SingleOrDefault();

            var application = GetApplicationsFromStore(applicationName).SingleOrDefault();

            if (string.Equals(applicationName, Constants.SYSTEM_APPLICATION_NAME, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new SettingsStoreException(Constants.ERROR_DIRECTORY_CANNOT_DELETE);
            }

            if (application == null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_APPLICATION_UNKNOWN);
            }

            if (directory != null)
            {
                throw new SettingsNotFoundException(Constants.ERROR_DIRECTORY_UNKOWN);
            }

            Repository.Context.Directories.Add(directory);
            Repository.Context.SaveChanges();
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

                DirectoryAccessData currentAccess = item.Access.FirstOrDefault(a => a.ApiKeyId == Auth.CurrentIdentity.Id);

                model.AllowCreate = currentAccess.AllowCreate;
                model.AllowDelete = currentAccess.AllowDelete;
                model.AllowWrite = currentAccess.AllowWrite;
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

            var data = (from dir in Repository.Context.Directories
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

            var applications = (from app in data
                                select new ApplicationModel
                                {
                                    Name = app.Name,

                                    Description = app.Description,

                                    Versions = GetVersions(app.Name),

                                    AllowEdit = (app.ApiKeys.FirstOrDefault(a => a.Id == Auth.CurrentIdentity.Id && a.AdminKey) != null
                                                       || true == Auth.IsMasterKey),

                                    Directories = GetDirectories(app.Name),
                                    Created = app.Created,
                                });

            if (!string.IsNullOrWhiteSpace(applicationName) && applications.SingleOrDefault() == null)
            {
                throw new SettingsNotFoundException(applicationName);
            }

            return applications;
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
            if (!Auth.IsMasterKey)
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Application, AuthorizationLevel.Create, applicationName, Auth.CurrentIdentity.Id);
            }

            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new SettingsStoreException(Constants.ERROR_APPLICATION_NO_NAME);
            }

            var application = Repository.Context.Applications.FirstOrDefault(app => app.Name == applicationName);

            if (application != null)
            {
                throw new SettingsStoreException(Constants.ERROR_APPLICATION_ALREADY_EXISTS);
            }

            application = new ApplicationData();
            var directory = new DirectoryData();
            using (TransactionScope scope = new TransactionScope())
            {
                application.Name = applicationName;

                if (string.IsNullOrWhiteSpace(applicationDescription))
                {
                    applicationDescription = Constants.DEAULT_APPLICATION_DESCRIPTION;
                }

                application.Description = applicationDescription;
                application.Created = DateTime.UtcNow;

                Repository.Context.Applications.Add(application);
                Repository.Context.SaveChanges();

                VersionData version = new VersionData { Version = 1, Created = DateTime.UtcNow, ApplicationId = application.Id };
                Repository.Context.Versions.Add(version);
                Repository.Context.SaveChanges();

                if (string.IsNullOrWhiteSpace(directoryName))
                {
                    directoryName = Constants.DEAULT_DIRECTORY_NAME;
                }

                if (string.IsNullOrWhiteSpace(directoryDescription))
                {
                    directoryDescription = Constants.DEAULT_DIRECTORY_DESCRIPTION;
                }

                directory = new DirectoryData();
                directory.Name = directoryName;
                directory.Description = directoryDescription;
                directory.ApplicationId = application.Id;
                directory.Created = DateTime.UtcNow;

                Repository.Context.Directories.Add(directory);
                Repository.Context.SaveChanges();

                ApiKeyData apiKey = new ApiKeyData { ApiKey = ApiKeyGenerator.Create(), Application = application, Active = true, AdminKey = true, Created = DateTime.UtcNow };

                Repository.Context.ApiKeys.Add(apiKey);
                Repository.Context.SaveChanges();

                DirectoryAccessData access = new DirectoryAccessData();
                access.AllowWrite = true;
                access.AllowDelete = true;
                access.AllowCreate = true;
                access.ApiKey = apiKey;
                access.Directory = directory;

                Repository.Context.Access.Add(access);

                Repository.Context.SaveChanges();

                scope.Complete();
            }

            //To load the autocreated accessrights.
            Repository.Context.Entry<DirectoryData>(directory).Collection("Access").Load();
            return GetApplication(applicationName);
        }

        private IEnumerable<ApplicationData> GetApplicationsFromStore(string applicationName)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
                applicationName = null;

            if (!AllowRead())
                throw new SettingsAuthorizationException(AuthorizationScope.Application, AuthorizationLevel.Read, "ApiKey", 0);

            var applications = (from app in Repository.Context.Applications
                                where (app.Name == applicationName || applicationName == null) &&
                                (app.ApiKeys.FirstOrDefault(a => a.Id == Auth.CurrentIdentity.Id) != null
                                || true == Auth.IsMasterKey)
                                select app);

            return applications;
        }

        public void DeleteApplication(string applicationName)
        {
            if (AllowDelete())
            {
                if (string.Equals(applicationName, Constants.SYSTEM_APPLICATION_NAME, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new SettingsStoreException(Constants.ERROR_APPLICATION_CANNOT_DELETED);
                }
                var application = Repository.Context.Applications.FirstOrDefault(a => a.Name == applicationName);

                if (application != null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var directories = Repository.Context.Directories.Where(d => d.ApplicationId == application.Id);

                        var settings = Repository.Context.Settings.Where(s => s.DirectoryId == application.Id);
                        Repository.Context.Settings.RemoveRange(settings);

                        Repository.Context.SaveChanges();

                        foreach (var item in directories)
                        {
                            DeleteDirectory(application.Name, item.Name);
                        }

                        var repositories = Repository.Context.Versions.Where(s => s.Application.Id == application.Id);
                        Repository.Context.Versions.RemoveRange(repositories);

                        Repository.Context.SaveChanges();

                        var keys = Repository.Context.ApiKeys.Where(k => k.Application.Id == application.Id);
                        Repository.Context.ApiKeys.RemoveRange(keys);
                        Repository.Context.SaveChanges();

                        Repository.Context.Applications.Remove(application);
                        Repository.Context.SaveChanges();

                        scope.Complete();
                    }
                }
            }
            else
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Application, AuthorizationLevel.Delete, applicationName, Auth.CurrentIdentity.Id);
            }
        }

        #endregion Application



        public bool AllowRead()
        {
            return Auth.CurrentApiKey != null;
        }

        public bool AllowDelete()
        {
            return Auth.IsMasterKey;
        }
    }
}