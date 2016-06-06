using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteCaching.Models
{
    public class Pedido
    {
        public Persona Persona { get; set; }
        public int PersonaId { get; set; }
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal Total { get; set; }
        public string NumeroDocumento { get; set; }
    }
}