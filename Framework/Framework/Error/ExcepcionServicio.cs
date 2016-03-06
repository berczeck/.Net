using System;

namespace Framework.Error
{
    [Serializable]
    public class ExcepcionServicio : Exception
    {
        public ExcepcionServicio(string mensaje)
            : base(mensaje)
        {
        }

        public ExcepcionServicio(string codigo, string mensaje)
            : base(mensaje)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}