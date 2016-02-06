using System.Collections.Generic;

namespace EfConvenciones.Generador
{
    public interface IInspectorPropiedades
    {
        Dictionary<string, object> RecuperarCamposActualizados<T>(T tipo);
    }
}