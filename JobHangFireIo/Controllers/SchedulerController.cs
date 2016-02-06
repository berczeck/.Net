using System;
using System.Web.Http;
using Hangfire;

namespace JobHangFireIo.Controllers
{
    [RoutePrefix("apiweb")]
    public class SchedulerController : ApiController
    {
        [Route("sche")]
        public string GetEjecutar()
        {

            for (int i = 0; i < 10; i++)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget"));
            }

            for (int i = 0; i < 10; i++)
            {
                BackgroundJob.Schedule(() => Console.WriteLine("Delayed"), TimeSpan.FromMinutes(2));
            }

            RecurringJob.AddOrUpdate(() => Console.Write("Recurring"), Cron.Daily);

            return "Ok";
        }
    }
}
