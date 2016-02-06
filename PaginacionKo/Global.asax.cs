using System;
using System.Web;
using System.Web.Http;

namespace PaginacionKo
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}