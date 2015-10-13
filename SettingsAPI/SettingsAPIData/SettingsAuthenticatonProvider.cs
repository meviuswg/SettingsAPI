using SettingsAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    internal class SettingsAuthenticatonProvider
    {
        private IApiKeyRepository repository;

        public SettingsAuthenticatonProvider(IApiKeyRepository repository)
        {
            this.repository = repository;
        }


        public bool ValidateAndSetPrincipal(object apiKey)
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

                //Force to reload any data of the key.
                repository.Invalidate(strKey);

                ApiKeyModel key = repository.GetKey(strKey);

                if (key != null && key.Active)
                {
                    string[] roles = SettingsAuthorizationRoleProvider.ConstructRoles(key);

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
