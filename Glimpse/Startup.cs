using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Glimpse.Startup))]
namespace Glimpse
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
