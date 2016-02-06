using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfConvenciones
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public long NumeroEmpleados { get; set; }
        public int? NumeroAscensores { get; set; }
    }
}
