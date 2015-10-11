using SettingsAPIData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SettingsAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
#if DEBUG
            SetupDebug();
#endif
        }
        private void SetupDebug()
        {
            Log.Logger = (ex) =>  Debug.WriteLine(ex);
            SettingsAPIData.SettingsStoreException.Log = (m) => Log.Message(m);

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            SetAPIKey();
        }


        private void SetAPIKey()
        {

            var localPath = HttpContext.Current.Request.Url.LocalPath.ToLower();

            if (!localPath.Contains("/help/api") && localPath.Contains("/api/"))
            {
                var queryString = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());

                if (queryString != null)
                {
                    var apiKeyValues = queryString.GetValues("ApiKey");

                    if (apiKeyValues != null)
                    {
                        var apiKey = apiKeyValues.GetValue(0);
                        HttpContext.Current.Cache["APIKEY"] = apiKey;

                        return;
                    }
                }

                HttpContext.Current.Response.StatusCode = 401;
                HttpContext.Current.Response.End();
            }
        }

    }
}
