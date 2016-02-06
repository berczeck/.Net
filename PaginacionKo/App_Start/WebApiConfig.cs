using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using PaginacionKo.Util;

namespace PaginacionKo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApiWithAction", "webApi/command/{controller}/{action}",
                new { action = "DefaultAction", controller = "ActivarController" });

            config.Routes.MapHttpRoute(
                "DefaultApiWithID",
                "webApi/query/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute("ActivarApi", "webApi/activar/e/{action}",
            //    new { action = "DefaultAction", controller = "ActivarController" }
            //    );

            //config.Routes.MapHttpRoute("DefaultApi", "webapi/{controller}/{id}",
            //    new {id = RouteParameter.Optional}
            //    );

            //config.Routes.MapHttpRoute("ProductSearchApi", "webApi/producto",
            //    new {controller = "ProductController"}
            //    );

            //config.Routes.MapHttpRoute("BandejaSearchApi", "webApi/bandeja/{action}",
            //    new {controller = "BandejaController", action = "DefaultAction"}
            //    );
        }
    }
}