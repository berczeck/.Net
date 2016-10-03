using System;
using System.Threading.Tasks;
using MassTransit;

namespace Core
{
    public class DocketAlertNotificationConsumer : IConsumer<DocketAlertNotificationRequest>
    {
        public async Task Consume(ConsumeContext<DocketAlertNotificationRequest> context)
        {
            await Console.Out.WriteLineAsync($"{nameof(DocketAlertNotificationRequest)}: {context.Message.DocketId} {DateTime.Now.ToString("hh:mm:ss:ms")} ThreadID:{System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
