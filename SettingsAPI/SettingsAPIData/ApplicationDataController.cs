using SettingsAPIData.Data;
using SettingsAPIData.Model;
using SettingsAPIData.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SettingsAPIData
{
    public class ApplicationDataController : BaseDataController
    {
        public ApplicationDataController(SettingsDbContext Context, IApiKey apikey)
            : base(Context, apikey)
        {

        }

        public IEnumerable<ApplicationModel> GetApplications()
        {
            var applications = (from app in Context.Applications
                                where (app.ApiKeys.FirstOrDefault(a => a.Id == CurrentIdentity) != null
                                || true == IsMasterKey)

                                select new ApplicationModel
                                {
                                    Name = app.Name,

                                    Description = app.Description,

                                    Versions = (from v in app.Repositories
                                                select new VersionModel
                                                {
                                                    Version = v.Version,
                                                    Created = v.Created
                                                }),

                                    EditDirectories = (app.ApiKeys.FirstOrDefault(a => a.Id == CurrentIdentity && a.EditDirectories) != null
                                                       || true == IsMasterKey),


                                    Directories = (from r in app.Directories
                                                   where (r.Access.FirstOrDefault(acc => acc.ApiKeyId == CurrentIdentity) != null
                                                    || true == IsMasterKey)
                                                   select
                                                   new DirectoryModel
                                                   {
                                                       Name = r.Name,
                                                       Description = r.Description,
                                                       Items = r.Settings.Count,
                                                       AllowCreate = (r.Access.FirstOrDefault(acc => acc.ApiKeyId == CurrentIdentity && acc.AllowCreate) != null
                                                           || true == IsMasterKey),
                                                       AllowWrite = (r.Access.FirstOrDefault(acc => acc.ApiKeyId == CurrentIdentity && acc.AllowDelete) != null
                                                           || true == IsMasterKey),
                                                       AllowDelete = (r.Access.FirstOrDefault(acc => acc.ApiKeyId == CurrentIdentity && acc.AllowDelete) != null
                                                           || true == IsMasterKey)
                                                   }),
                                    Created = app.Created,
                                });

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
            if(!IsMasterKey)
            {
                throw new SettingsAuthorizationException(AuthorizationScope.Application, AuthorizationLevel.Create, applicationName, CurrentIdentity);
            }

            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new SettingsStoreException("Invalid application name.");
            }

            var application = Context.Applications.FirstOrDefault(app => app.Name == applicationName);

            if(application != null)
            {
                throw new SettingsStoreException("Application name allready in use.");
            }

            using (TransactionScope scope = new TransactionScope())
            {

                application = new ApplicationData();

                application.Name = applicationName;

                if (string.IsNullOrWhiteSpace(applicationDescription))
                {
                    applicationDescription = Constants.DEAULT_APPLICATION_DESCRIPTION;
                }

                application.Description = applicationDescription;
                application.Created = DateTime.UtcNow;

                Context.Applications.Add(application);
                Context.SaveChanges();

                RepositoryData repository = new RepositoryData { Version = 1, Created = DateTime.UtcNow, ApplicationId = application.Id };
                Context.Repositories.Add(repository);
                Context.SaveChanges();

                repository = Context.Repositories.FirstOrDefault(r => r.ApplicationId == application.Id && r.Version == 1);

                if (string.IsNullOrWhiteSpace(directoryName))
                {
                    directoryName = Constants.DEAULT_DIRECTORY_NAME;
                }

                if (string.IsNullOrWhiteSpace(directoryDescription))
                {
                    directoryDescription = Constants.DEAULT_DIRECTORY_DESCRIPTION;
                }

                Directorydata directory = new Directorydata();
                directory.Name = directoryName;
                directory.Description = directoryDescription;
                directory.Application = application;
        
                Context.Directories.Add(directory);
                Context.SaveChanges();

                directory = Context.Directories.FirstOrDefault(d => d.ApplicationId == application.Id && d.Name == directory.Name);

                ApiKeyData apiKey = new ApiKeyData { ApiKey = ApiKeyGenerator.Create(), Application = application, Active = true, EditDirectories = true, Created = DateTime.UtcNow };

                Context.ApiKeys.Add(apiKey);
                Context.SaveChanges();

                apiKey = Context.ApiKeys.FirstOrDefault(a => a.ApiKey == apiKey.ApiKey);

                DirectoryAccessData access = new DirectoryAccessData();
                access.AllowWrite = true;
                access.AllowDelete = true;
                access.AllowCreate = true;
                access.ApiKey = apiKey;
                access.Directory = directory;

                Context.Access.Add(access);

                Context.SaveChanges();

                scope.Complete();
            }

            return GetApplications().Where(a => a.Name == applicationName).FirstOrDefault();
         
        }
    }
}
