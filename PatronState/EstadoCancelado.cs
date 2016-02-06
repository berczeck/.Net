using System;

namespace PatronState
{
    public class EstadoCancelado : IEstadoPedido
    {
        private FlujoPedido Contexto;

        public void Ejecutar(FlujoPedido contexto)
        {
            Contexto = contexto;
            if (contexto.Pedido.EstadoPropuestoId != PedidoEstado.Reiniciado)
            {
                throw new Exception("Te crees pendejo");
            }
            contexto.Pedido.EstadoId = contexto.Pedido.EstadoPropuestoId;
            Grabar(contexto.Pedido);
        }

        private void Grabar(Pedido pedido)
        {
        }
    }
}