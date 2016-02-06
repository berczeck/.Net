namespace PatronState
{
    public class Pedido
    {
        public int Id { get; set; }
        public PedidoEstado EstadoId { get; set; }
        public PedidoEstado EstadoPropuestoId { get; set; }
    }
}