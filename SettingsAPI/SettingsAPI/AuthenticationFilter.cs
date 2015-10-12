using SettingsAPIData;
using SettingsAPIData.Data;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Linq;
using System.Security;
using System.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Ninject;
using System.Web.Http;
using System.Web.Http.Filters;
using SettingsAPIData.Model;

namespace SettingsAPI
{
    public class AuthenticationFilter : IAuthenticationFilter 
    {

        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }
        [Inject]
        public ISettingsAuthorizationProvider Auth { get; set; }
 
        public void Authenticate(HttpAuthenticationContext context)
        {
            if (context == null)
                return;

            if (context.Principal != null && context.Principal.Identity.IsAuthenticated)
                return;

            var queryString = HttpUtility.ParseQueryString(context.Request.RequestUri.Query.ToString());

            if (queryString != null)
            {
                var apiKeyValues = queryString.GetValues("ApiKey");

                if (apiKeyValues != null)
                {
                    var apiKey = apiKeyValues.GetValue(0);

                    IPrincipal principal = null;
                    if (Auth.Validate(apiKey, out principal))
                    {
                        context.Principal = principal;
                        Thread.CurrentPrincipal = principal;
                    }
                }
            }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            return Task.Run(() => Authenticate(context), cancellationToken);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.Run(() => Authorize(context), cancellationToken);
        }

        private void Authorize(HttpAuthenticationChallengeContext context)
        {

        }
    }
}