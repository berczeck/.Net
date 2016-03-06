using System;
using Akka.Actor;
using Comentario.Core;

namespace Comentario.Web.Models
{
    public class PanelRespuestaActor : ReceiveActor
    {
        public PanelRespuestaActor()
        {
            Receive<ResponseObtenerComentarios>(comentarios =>
            {
                var lista = comentarios.Comentarios;
                var tipo = lista.ToString();
            });

            Receive<string>(comentario =>
            {
                Actores.PanelComentarioControlador.Tell(new ObtenerComentarios());
            });
            
            Receive<Core.Comentario>(comentario =>
            {
                Actores.PanelComentarioControlador.Tell(comentario);
            });
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromSeconds(4),
                decider: Decider.From(x =>
                {
                    //Maybe we consider ArithmeticException to not be application critical
                    //so we just ignore the error and keep going.
                    if (x is ArithmeticException) return Directive.Resume;

                    //Error that we cannot recover from, stop the failing actor
                    else if (x is NotSupportedException) return Directive.Stop;

                    //In all other cases, just restart the failing actor
                    else return Directive.Restart;
                }));
        }
    }
}