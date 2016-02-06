using System;

namespace PatronState
{
    public class EstadoEmitido : IEstadoPedido
    {
        private FlujoPedido Contexto;

        public void Ejecutar(FlujoPedido contexto)
        {
            Contexto = contexto;
            if (contexto.Pedido.EstadoPropuestoId != PedidoEstado.Revisado ||
                contexto.Pedido.EstadoPropuestoId != PedidoEstado.Anulado)
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