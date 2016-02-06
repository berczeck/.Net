using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfConvenciones
{
    public class Organizacion
    {
        public short PaisId { get; set; }
        public Pais Pais { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public string Titulo { get; set; }
    }
}
