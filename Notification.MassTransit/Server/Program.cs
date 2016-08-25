using System;
using Autofac;
using Core;
using log4net.Config;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using Server.ExceptionLoger;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            Log4NetLogger.Use();

            var container = CreateContainer();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.UseJsonSerializer();
                cfg.UseExceptionLogger();
                cfg.UseRetry(Retry.Immediate(2));
                cfg.PrefetchCount = 100;

                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.ReceiveEndpoint(host, "DocketLinkNotification", ep => 
                {
                    ep.UseRetry(Retry.Interval(3, TimeSpan.FromSeconds(2)));
                    ep.Consumer(() => container.Resolve<SendDocketLinkNotificationConsumer>());
                });
                cfg.ReceiveEndpoint(host, "KeyDocumentLinkNotification", ep =>
                {
                    ep.Consumer(() => container.Resolve<SendKeyDocumentLinkNotificationConsumer>());
                });
                cfg.ReceiveEndpoint(host, "DocketAlertNotification", ep =>
                {
                    ep.UseRateLimit(1, TimeSpan.FromSeconds(5));
                    ep.Consumer(() => container.Resolve<DocketAlertNotificationConsumer>());
                });
            });

            busControl.Start();

            Console.WriteLine("Start server....");
            Console.ReadLine();

            busControl.Stop();
        }
        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ConsumerModule>();
            return builder.Build();
        }
    }

    class ConsumerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConsumers(typeof(SendDocketLinkNotificationConsumer).Assembly);
        }
    }
    
}

