using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PatronStrategy;

namespace Glimpse
{
    public class Contenedor: NinjectModule
    {
        public override void Load()
        {
            Bind<ICobroRegistro>().To<CobroRegistroPersonaNatural>();
        }
    }
}