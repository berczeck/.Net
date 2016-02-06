using System.Collections.Generic;

namespace EfConvenciones.Generador
{
    public class InspectorDiccionario : IInspectorPropiedades
    {
        public Dictionary<string, object> RecuperarCamposActualizados<T>(T tipo)
        {
            return tipo as Dictionary<string, object>;
        }
    }
}
