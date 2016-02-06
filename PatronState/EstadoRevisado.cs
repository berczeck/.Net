using System;

namespace PatronState
{
    public class EstadoRevisado : IEstadoPedido
    {
        private FlujoPedido Contexto;

        public void Ejecutar(FlujoPedido contexto)
        {
            Contexto = contexto;
            if (contexto.Pedido.EstadoPropuestoId != PedidoEstado.Aprobado ||
                contexto.Pedido.EstadoPropuestoId != PedidoEstado.Rechazado)
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