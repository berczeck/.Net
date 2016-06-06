using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteCaching.Startup))]
namespace SiteCaching
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
