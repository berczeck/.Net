namespace PatronState
{
    public class EstadoInicial : IEstadoPedido
    {
        private FlujoPedido Contexto;

        public void Ejecutar(FlujoPedido contexto)
        {
            Contexto = contexto;
            contexto.Pedido.EstadoId = PedidoEstado.Emitido;
            Grabar(contexto.Pedido);
        }

        private void Grabar(Pedido pedido)
        {
        }
    }
}