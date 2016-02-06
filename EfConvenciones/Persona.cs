using System;

namespace EfConvenciones
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public short PaisId { get; set; }
        public Pais Pais { get; set; }

        public int? EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
