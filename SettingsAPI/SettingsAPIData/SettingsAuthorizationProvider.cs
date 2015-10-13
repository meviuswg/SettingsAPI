using SettingsAPIData.Model;
using SettingsAPIShared;
using System.Security.Principal;

namespace SettingsAPIData
{
    /// <summary>
    /// Authorization against the current thread principal.
    /// </summary>
    public class SettingsAuthorizationProvider : ISettingsAuthorizationProvider
    {
        private IApiKeyRepository repository;
        private SettingsAuthenticatonProvider authenticator;

        public SettingsAuthorizationProvider(IApiKeyRepository repository)
        {
            this.repository = repository;
            this.authenticator = new SettingsAuthenticatonProvider(repository);
        }

        public IApiKey CurrentApiKey
        {
            get
            {
                if (CurrentIdentity != null)
                {
                    return new ApiKey(CurrentIdentity.Name);
                }

                return null;
            }
        }

        public ApiIdentity CurrentIdentity
        {
            get
            {
                if (User.Identity is ApiIdentity)
                {
                    return (ApiIdentity)User.Identity;
                }

                return null;
            }
        }

        public bool IsMasterKey
        {
            get
            {
                if (CurrentIdentity != null)
                {
                    return CurrentIdentity.Id == Constants.SYSTEM_MASTER_KEY_ID;
                }
                return false;
            }
        }

        public IPrincipal User
        {
            get
            {
                return System.Threading.Thread.CurrentPrincipal;
            }
        }

        public bool AllowCreateApplication(string application)
        {
            return User.IsInRole(SecurityRoles.RoleCreateApplication());
        }

        public bool AllowCreateDirectories(string application)
        {
            return User.IsInRole(SecurityRoles.RoleCreateDirectory(application));
        }

        public bool AllowCreateDirectories(string application, string directoryName)
        {
            return User.IsInRole(SecurityRoles.RoleCreateDirectory(application));
        }

        public bool AllowCreateDirectory(string application, string directoryName)
        {
            if (!string.IsNullOrWhiteSpace(directoryName))
            {
                if (directoryName.StartsWith("_"))
                {
                    return false;
                }
                return User.IsInRole(SecurityRoles.RoleCreateDirectory(application));
            }

            return false;
        }

        public bool AllowCreateSetting(string application, string directoryName)
        {
            return User.IsInRole(SecurityRoles.RoleCreateSetting(application, directoryName));
        }

        public bool AllowCreateVersion(string application)
        {
            return User.IsInRole(SecurityRoles.RoleCreateVersion(application));
        }

        public bool AllowDeleteApplication(string application)
        {
            return User.IsInRole(SecurityRoles.RoleDeleteApplication(application));
        }

        public bool AllowDeleteDirectory(string application, string directoryName)
        {
            return User.IsInRole(SecurityRoles.RoleDeleteDirectory(application, directoryName));
        }

        public bool AllowDeleteSetting(string application, string directoryName)
        {
            return User.IsInRole(SecurityRoles.RoleDeleteSetting(application, directoryName));
        }

        public bool AllowDeleteVersion(string application)
        {
            return User.IsInRole(SecurityRoles.RoleCreateVersion(application));
        }

        public bool AllowWriteSetting(string application, string directoryName)
        {
            return User.IsInRole(SecurityRoles.RoleWriteSetting(application, directoryName));
        }

        public string[] GetApiRoles(ApiKeyModel data)
        {
            return SettingsAuthorizationRoleProvider.ConstructRoles(data);
        }

        public void Invalidate()
        {
            if (CurrentIdentity != null)
                repository.Invalidate(CurrentIdentity.Name);
        }

        public bool Validate(object apiKey)
        {
            return authenticator.ValidateAndSetPrincipal(apiKey);
        }

        public bool Validate(object apiKey, out IPrincipal principal)
        {
            return authenticator.Validate(apiKey, out principal);
        }
    }
}