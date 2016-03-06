using System;

namespace Framework.Error
{
    [Serializable]
    public class ExcepcionActualizarRegistro : Exception
    {
        public ExcepcionActualizarRegistro(string message)
            : base(message)
        {
        }
    }
}