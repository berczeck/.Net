using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ApiOauthAuth.Startup))]

namespace ApiOauthAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}