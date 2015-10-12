using System.Web.Http;

namespace SettingsAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new AuthenticationFilter());
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{name}",
                defaults: new { name = RouteParameter.Optional }
            );
        }
    }
}