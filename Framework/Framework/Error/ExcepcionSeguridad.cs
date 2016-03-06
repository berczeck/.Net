using System;

namespace Framework.Error
{
    [Serializable]
    public class ExcepcionSeguridad : Exception
    {
        public ExcepcionSeguridad(string mensaje)
            : base(mensaje)
        {
        }

        public ExcepcionSeguridad(string codigo, string mensaje)
            : base(mensaje)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}
