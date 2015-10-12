using SettingsAPIData.Model;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;

namespace SettingsAPIData
{
    /// <summary>
    /// Authorization against the current thread principal.
    /// </summary>
    public class SettingsAuthorizationProvider : ISettingsAuthorizationProvider
    {
        private IApiKeyRepository repository;

        public SettingsAuthorizationProvider(IApiKeyRepository repository)
        {
            this.repository = repository;
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
            return User.IsInRole(SecurityRoles.RoleCreateApplication(application));
        }

        public bool AllowCreateDirectory(string application, string directoryName)
        {
            if (!string.IsNullOrWhiteSpace(directoryName))
            {
                if (directoryName.StartsWith("_"))
                {
                    return false;
                }
                return User.IsInRole(SecurityRoles.RoleCreateDirectory(application, directoryName));
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
            List<string> roles = new List<string>();

            if (data != null && data.Active)
            {
                if (data.AdminKey || data.Id == Constants.SYSTEM_MASTER_KEY_ID)
                {
                    roles.Add(Constants.SECURITY_ROLE_ADMIN);
                }

                if (data.Id == Constants.SYSTEM_MASTER_KEY_ID)
                    roles.Add(Constants.SECURITY_ROLE_MASTER);

                foreach (var item in data.Access)
                {
                    if (item.AllowCreate)
                    {
                        roles.Add(SecurityRoles.RoleCreateSetting(item.ApplicationName, item.DirectoryName));
                    }

                    if (item.AllowDelete)
                    {
                        roles.Add(SecurityRoles.RoleDeleteSetting(item.ApplicationName, item.DirectoryName));
                    }

                    if (item.AllowWrite)
                    {
                        roles.Add(SecurityRoles.RoleWriteSetting(item.ApplicationName, item.DirectoryName));
                    }
                }
            }

            return roles.ToArray();
        }

        public bool Validate(object apiKey)
        {
            IPrincipal principal;
            bool validationResult = Validate(apiKey, out principal);

            if (validationResult)
            {
                System.Threading.Thread.CurrentPrincipal = principal;
            }

            return validationResult;
        }

        public bool Validate(object apiKey, out IPrincipal principal)
        {
            try
            {
                principal = null;

                string strKey = null;

                if (apiKey != null)
                {
                    strKey = apiKey.ToString();

                    if (string.IsNullOrWhiteSpace(strKey))
                        return false;
                }
                ApiKeyModel key = repository.GetKey(strKey);

                if (key != null && key.Active)
                {
                    string[] roles = GetApiRoles(key);

                    principal = (IPrincipal)new GenericPrincipal(new ApiIdentity(apiKey.ToString(), key.Id), roles.ToArray());
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                throw new AuthenticationException("Could not authenticate");
            }
        }
    }
}