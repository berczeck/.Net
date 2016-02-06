using System;
using System.Collections.Generic;

namespace PatronState
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Pedido pedido = null;

            var flujoContexto = new FlujoPedido(pedido);

            flujoContexto.ListaEstados =
                new Dictionary<PedidoEstado, IEstadoPedido>
                {
                    {PedidoEstado.Inicial, new EstadoInicial()},
                    {PedidoEstado.Emitido, new EstadoEmitido()},
                    {PedidoEstado.Revisado, new EstadoRevisado()},
                    {PedidoEstado.Rechazado, new EstadoCancelado()},
                    {PedidoEstado.Reiniciado, new EstadoEmitido()}
                };

            flujoContexto.Procesar();

            Console.ReadLine();
        }
    }
}