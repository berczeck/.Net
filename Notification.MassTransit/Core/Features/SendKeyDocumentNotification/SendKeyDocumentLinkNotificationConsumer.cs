using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;

namespace Core
{
    public class SendKeyDocumentLinkNotificationConsumer : IConsumer<SendKeyDocumentLinkNotificationRequest>
    {        
        public async Task Consume(ConsumeContext<SendKeyDocumentLinkNotificationRequest> context)
        {
            await Console.Out.WriteLineAsync($"{nameof(SendKeyDocumentLinkNotificationRequest)}: {context.Message.DocumentId} {DateTime.Now.ToString("hh:mm:ss:ms")} ThreadID:{System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
