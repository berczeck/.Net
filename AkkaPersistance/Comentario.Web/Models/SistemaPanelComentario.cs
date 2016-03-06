using System;
using Akka.Actor;
using Comentario.Core;

namespace Comentario.Web.Models
{
    public class SistemaPanelComentario
    {
        private static ActorSystem ActorSystem;

        public static void CreaContexto()
        {
            ActorSystem = ActorSystem.Create("SistemaPanelComentario");

            //Actores.PanelComentarioControlador = 
            //    ActorSystem.ActorSelection("akka.tcp://SistemaPanelComentario@127.0.0.1:8091/user/PanelComentarioControlador")
            //    .ResolveOne(TimeSpan.FromSeconds(3))
            //    .Result;

            Actores.CuentaAhorroControlador =
                ActorSystem.ActorSelection("akka.tcp://SistemaPanelComentario@127.0.0.1:8091/user/CuentaAhorroControlador")
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result;

            //Actores.PanelRespuestaControlador = ActorSystem.ActorOf<PanelRespuestaActor>();
            Actores.CuentaAhorroRespuestaControlador = ActorSystem.ActorOf<CuentaAhorroActorLocal>();
        }

        public static void ApagarContexto()
        {
            ActorSystem.Terminate();
            ActorSystem.AwaitTermination(TimeSpan.FromSeconds(1));            
        }
    }

    public static class Actores
    {
        public static IActorRef PanelComentarioControlador { get; set; }
        public static IActorRef PanelRespuestaControlador { get; set; }

        public static IActorRef CuentaAhorroRespuestaControlador { get; set; }
        public static IActorRef CuentaAhorroControlador { get; set; }
    }
}