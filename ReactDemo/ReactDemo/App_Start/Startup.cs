using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(ReactDemo.Startup))]

namespace ReactDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}