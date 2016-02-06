using System.Collections.Generic;

namespace EfConvenciones.Generador
{
    public class InspectorTipoNativo : IInspectorPropiedades
    {
        private readonly string _nombreColumna;

        public InspectorTipoNativo(string nombre)
        {
            _nombreColumna = nombre;
        }

        public Dictionary<string, object> RecuperarCamposActualizados<T>(T tipo)
        {
            return new Dictionary<string, object>
            {
                {_nombreColumna, tipo}
            };
        }
    }
}
