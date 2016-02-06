using System;
using System.Collections.Generic;

namespace PatronState
{
    public class FlujoPedido
    {
        public FlujoPedido(Pedido pedido)
        {
            Pedido = pedido;

            if (pedido.EstadoId == PedidoEstado.Inicial)
            {
                EstadoActual = ListaEstados[PedidoEstado.Inicial];
            }
            else
            {
                PedidoActual = ObtenerPedido(pedido.Id);

                if (!ListaEstados.ContainsKey(PedidoActual.EstadoId))
                {
                    throw new Exception("Te crees pendejo");
                }
                EstadoActual = ListaEstados[PedidoActual.EstadoId];
            }
        }

        private IEstadoPedido EstadoActual { get; set; }
        public Pedido Pedido { get; set; }
        public Pedido PedidoActual { get; set; }

        public Dictionary<PedidoEstado, IEstadoPedido> ListaEstados { get; set; }

        public void Procesar()
        {
            EstadoActual.Ejecutar(this);
        }

        public Pedido ObtenerPedido(int id)
        {
            //Desde la BD
            return new Pedido();
        }
    }
}