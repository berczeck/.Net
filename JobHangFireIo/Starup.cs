
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(JobHangFireIo.Startup))]

namespace JobHangFireIo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfire(config =>
            {
                config.UseSqlServerStorage("JobHangFireIoDb");
                config.UseServer();
            });
        }
    }
}