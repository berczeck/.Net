using System;
using System.Threading.Tasks;
using MassTransit;

namespace Core
{
    public class SendDocketLinkNotificationConsumer : IConsumer<SendDocketLinkNotificationRequest>
    {
        public async Task Consume(ConsumeContext<SendDocketLinkNotificationRequest> context)
        {
            await Console.Out.WriteLineAsync($"{nameof(SendDocketLinkNotificationRequest)}: Intento:{context.GetRetryAttempt()} DocketId: {context.Message.DocketId} {DateTime.Now.ToString("hh:mm:ss:ms")} ThreadID:{System.Threading.Thread.CurrentThread.ManagedThreadId}");


            throw new Exception("Error");
        }
    }
}
