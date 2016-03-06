using System;

namespace Framework.Error
{
    [Serializable]
    public class ExcepcionReglaNegocio : Exception
    {
        public ExcepcionReglaNegocio(string mensaje, Exception error)
            : base(mensaje, error)
        {
        }

        public ExcepcionReglaNegocio(string mensaje)
            : base(mensaje)
        {
        }

        public ExcepcionReglaNegocio(string codigo, string mensaje, Exception error)
            : base(mensaje, error)
        {
            Codigo = codigo;
        }

        public ExcepcionReglaNegocio(string codigo, string mensaje)
            : base(mensaje)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}