using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ReactDemo
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(new UtplCustomHandlerError()); 
            GlobalFilters.Filters.Add(new IGCustomHandlerError());


            ServiceBusConfiguracion.CrearCola();
            ServiceBusConfiguracion.RecibirMensaje();
        }

        protected void Application_Error()
        {
            var error = Server.GetLastError();
            var mensaje = error.ToString();
        }
    }
}
