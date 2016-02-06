using System.Web.Http;

namespace AutoCompleteTockenInput.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "webapi/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }
    }
}