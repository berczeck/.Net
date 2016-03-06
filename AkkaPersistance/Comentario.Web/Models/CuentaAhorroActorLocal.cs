using Akka.Actor;
using Comentario.Core;

namespace Comentario.Web.Models
{
    public class CuentaAhorroActorLocal : ReceiveActor
    {
        public CuentaAhorroActorLocal()
        {
            Receive<Deposito>(deposito =>
            {
                Actores.CuentaAhorroControlador.Tell(new ComandoRealizarDeposito(deposito));
            });
        }
    }
}