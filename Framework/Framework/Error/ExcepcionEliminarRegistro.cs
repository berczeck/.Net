using System;

namespace Framework.Error
{
    [Serializable]
    public class ExcepcionEliminarRegistro : Exception
    {
        public ExcepcionEliminarRegistro(string message)
            : base(message)
        {
        }
    }
}