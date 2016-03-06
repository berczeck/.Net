using System;

namespace Framework.Error
{
    public class ExcepcionValidacion : Exception
    {
        public ExcepcionValidacion(string mensaje)
            : base(mensaje)
        {
        }

        public ExcepcionValidacion(string codigo, string mensaje)
            : base(mensaje)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}
