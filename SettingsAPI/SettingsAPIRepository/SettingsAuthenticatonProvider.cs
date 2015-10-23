using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;

namespace SettingsAPIRepository
{
    internal class SettingsAuthenticatonProvider
    {
        private IValidationRepository repository;

        public SettingsAuthenticatonProvider(IValidationRepository repository)
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

                int keyId = 0;
                if (repository.IsValid(strKey, out keyId))
                {
                    string[] roles = SettingsAuthorizationRoleProvider.ConstructRoles(strKey);

                    repository.SetUsed(strKey);

                    principal = (IPrincipal)new GenericPrincipal(new ApiIdentity(apiKey.ToString(), keyId), roles.ToArray());
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