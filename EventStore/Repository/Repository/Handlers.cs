using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IHandler<in T> where T : IEvent
    {
        void Hanlder(T evento);
    }


    public class HandlerCantidadIncrementada : IHandler<CantidadIncrementada>
    {
        public void Hanlder(CantidadIncrementada evento)
        {
            //Envio coreo
            //ETC
        }
    }

    public class HandlerCantidadDecrementada : IHandler<CantidadDecrementada>
    {
        public void Hanlder(CantidadDecrementada evento)
        {
            //Envio coreo
            //ETC
        }
    }
}
