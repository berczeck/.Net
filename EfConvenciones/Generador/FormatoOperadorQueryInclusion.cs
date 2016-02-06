using System.Collections.Generic;
using System.Linq;

namespace EfConvenciones.Generador
{

    public class FormatoOperadorQueryInclusion : FormatoOperadorPlantilla
    {
        protected override string Operador
        {
            get { return "AND"; }
        }
    }
}
