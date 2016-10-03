using System;
using Core;
using MassTransit;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.UseJsonSerializer();

                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });              

            }
            );
            
            busControl.Start();

            busControl.Publish(new DocketAlertNotificationRequest(456));
            busControl.Publish<SendDocketLinkNotificationRequest>(new { DocketId = 1 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 2 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 3 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 4 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 5 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 6 });
            busControl.Publish(new DocketAlertNotificationRequest(547));
            busControl.Publish(new DocketAlertNotificationRequest(965));
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 12 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 13 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 14 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 15 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 16 });

            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 22 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 23 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 24 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 25 });
            busControl.Publish<SendKeyDocumentLinkNotificationRequest>(new { DocumentId = 26 });

            Console.WriteLine("Start Client....");
            Console.ReadLine();

            busControl.Stop();
        }
    }
}
