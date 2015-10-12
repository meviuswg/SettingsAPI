using System.Threading;

namespace SettingsAPIData
{
    public class PrincipalApiKey : IApiKey
    {
        private bool _authenticated;

        public string Key
        {
            get
            {
                if (!_authenticated)
                {
                    if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    {
                        throw new SettingsAuthorizationException(AuthorizationScope.Application, AuthorizationLevel.Read, "Api", 0);
                    }

                    _authenticated = true;
                }

                return Thread.CurrentPrincipal.Identity.Name;
            }
        }
    }
}