using System;
using Akka.Actor;
using Comentario.Core;
using MongoDB.Bson.Serialization;

namespace Comentario.Host
{
    class Program
    {
        static void Main(string[] args)
        {

            BsonClassMap.RegisterClassMap<Core.Comentario>();
            BsonClassMap.RegisterClassMap<Core.Deposito>();

            CrearContextoActores();
        }

        private static void CrearContextoActores()
        {
            using (var system = ActorSystem.Create("SistemaPanelComentario"))
            {
                //system.ActorOf<PanelComentarioActor>("PanelComentarioControlador");
                system.ActorOf<CuentaAhorroActor>("CuentaAhorroControlador");

                Console.ReadKey();
            }

            Console.ReadLine();
        }
    }
}
