using SettingsAPIRepository.Model;
using SettingsAPIShared;
using System.Security.Principal;
using System;
using SettingsAPIRepository.Data;

namespace SettingsAPIRepository
{
    /// <summary>
    /// Authorization against the current thread principal.
    /// </summary>
    public class SettingsAuthorizationProvider : ISettingsAuthorizationProvider
    {
        private SettingsAuthenticatonProvider authenticator;
        private IValidationRepository repository;

        public SettingsAuthorizationProvider(IValidationRepository repository)
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
            if (string.IsNullOrWhiteSpace(application))
                return false;

            return User.IsInRole(SecurityRoles.RoleCreateApplication()) || IsMasterKey;
        }

        public bool AllowCreateDirectories(string application)
        {
            if (string.IsNullOrWhiteSpace(application))
                return false;

            return User.IsInRole(SecurityRoles.RoleCreateDirectory(application)) || IsMasterKey;
        }

        public bool AllowCreateDirectories(string application, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(application) || string.IsNullOrWhiteSpace(directoryName))
                return false;

            return User.IsInRole(SecurityRoles.RoleCreateDirectory(application)) || IsMasterKey;
        }

        public bool AllowCreateDirectory(string application, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(application) || string.IsNullOrWhiteSpace(directoryName))
                return false;

            if (!string.IsNullOrWhiteSpace(directoryName))
            {
                if (string.Equals(directoryName, Constants.DEAULT_DIRECTORY_NAME, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
                return User.IsInRole(SecurityRoles.RoleCreateDirectory(application));
            }

            return false;
        }

        public bool AllowCreateSetting(string application, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(application) || string.IsNullOrWhiteSpace(directoryName))
                return false;

            return User.IsInRole(SecurityRoles.RoleCreateSetting(application, directoryName)) || IsMasterKey;
        }

        public bool AllowCreateVersion(string application)
        {
            if (string.IsNullOrWhiteSpace(application))
                return false;

            return User.IsInRole(SecurityRoles.RoleCreateVersion(application)) || IsMasterKey;
        }

        public bool AllowDeleteApplication(string application)
        {
            if (string.IsNullOrWhiteSpace(application) || string.Equals(application, Constants.SYSTEM_APPLICATION_NAME, System.StringComparison.CurrentCultureIgnoreCase))
                return false;

            return User.IsInRole(SecurityRoles.RoleDeleteApplication(application)) || IsMasterKey;
        }

        public bool AllowDeleteDirectory(string application, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(application) || string.IsNullOrWhiteSpace(directoryName))
                return false;

            if (!IsMasterKey && string.Equals(directoryName, Constants.DEAULT_DIRECTORY_NAME, System.StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            if(string.Equals(application, Constants.SYSTEM_APPLICATION_NAME, System.StringComparison.CurrentCultureIgnoreCase) && string.Equals(directoryName, Constants.DEAULT_DIRECTORY_NAME, System.StringComparison.CurrentCultureIgnoreCase))
                return false;

            return User.IsInRole(SecurityRoles.RoleDeleteDirectory(application, directoryName)) || IsMasterKey;
        }

        public bool AllowDeleteSetting(string application, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(application) || string.IsNullOrWhiteSpace(directoryName))
                return false;

            return User.IsInRole(SecurityRoles.RoleDeleteSetting(application, directoryName)) || IsMasterKey;
        }

        public bool AllowDeleteVersion(string application)
        {
            if (string.IsNullOrWhiteSpace(application))
                return false;

            return User.IsInRole(SecurityRoles.RoleCreateVersion(application)) || IsMasterKey;
        }

        public bool AllowReadDirectories(string application)
        {
            if (string.IsNullOrWhiteSpace(application))
                return false;

            return User.IsInRole(SecurityRoles.RoleReadDirectories(application)) || IsMasterKey;
        }

        public bool AllowReadDirectory(string application, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(application) || string.IsNullOrWhiteSpace(directoryName))
                return false;

            return User.IsInRole(SecurityRoles.RoleReadDirectory(application, directoryName)) || IsMasterKey;
        }

        public bool AllowReadVersions(string application)
        {
            if (string.IsNullOrWhiteSpace(application))
                return false;

            return User.IsInRole(SecurityRoles.RoleReadVersions(application)) || IsMasterKey;
        }

        public bool AllowEditApiKeys(string applicationName)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
                return false;

            return User.IsInRole(SecurityRoles.RoleEditApiKey(applicationName)) || IsMasterKey;
        } 

        public bool AllowReadApiKeys(string applicationName)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
                return false;

            return User.IsInRole(SecurityRoles.RoleReadApiKeys(applicationName)) || IsMasterKey;
        }

        public bool AllowriteSetting(string application, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(application) || string.IsNullOrWhiteSpace(directoryName))
                return false;

            return User.IsInRole(SecurityRoles.RoleWriteSetting(application, directoryName)) || IsMasterKey;
        }

        public string[] GetApiRoles(string apiKey)
        {
            return SettingsAuthorizationRoleProvider.ConstructRoles(apiKey);
        }

        public void Invalidate()
        {
            
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