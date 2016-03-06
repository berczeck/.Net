using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReactDemo
{
    using System.Configuration;
    using Microsoft.AspNet.SignalR;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Models;
    using ReactDemo.Hubs;

    public class ServiceBusConfiguracion
    {

        private static string connectionString =
            ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];

        public static void CrearCola()
        {
            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.QueueExists("Comentarios"))
            {
                namespaceManager.CreateQueue("Comentarios");
            }
        }

        public static void EnviarMensaje<T>(T comentario)
        {
            QueueClient Client =
                QueueClient.CreateFromConnectionString(connectionString, "Comentarios");

            Client.Send(new BrokeredMessage(comentario));
        }

        public static void RecibirMensaje()
        {
            QueueClient Client =
                QueueClient.CreateFromConnectionString(connectionString, "Comentarios");

            // Configure the callback options.
            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            // Callback to handle received messages.
            Client.OnMessage((message) =>
            {
                try
                {
                    //// Process message from queue.
                    //Console.WriteLine("Body: " + message.GetBody<string>());
                    //Console.WriteLine("MessageID: " + message.MessageId);
                    //Console.WriteLine("Test Property: " +
                    //                  message.Properties["TestProperty"]);

                    var comment = message.GetBody<CommentModel>();
                    comment.FechaHora = DateTime.Now.ToString("hh:mm:ss yyyyMMdd");
                    var repositorio = new RepositorioComentarios();
                    repositorio.Agregar(comment);

                    // Remove message from queue.
                    message.Complete();

                    NotificarComentarioNuevo(comment);
                }
                catch (Exception)
                {
                    // Indicates a problem, unlock message in queue.
                    message.Abandon();
                }
            }, options);
        }

        private static void NotificarComentarioNuevo(CommentModel comentario)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<CommentHub>();
            hubContext.Clients.All.loadComments(comentario);
        }

        private static void NotificarComentarioNuevo()
        {
            var repositorio = new RepositorioComentarios();
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<CommentHub>();
            hubContext.Clients.All.loadComments(repositorio.TraerTodo().Reverse());
        }
    }
}