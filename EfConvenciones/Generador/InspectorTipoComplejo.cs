using System;
using System.Collections.Generic;
using System.Linq;

namespace EfConvenciones.Generador
{
    public class InspectorTipoComplejo : IInspectorPropiedades
    {
        public Dictionary<string, dynamic> RecuperarCamposActualizados<T>(T param)
        {
            Type tipo = param.GetType();
            var camposFiltro = new Dictionary<string, dynamic>();

            foreach (var propiedad in tipo.GetProperties().Where(x => !Constantes.ValoresPorDefecto.ContainsKey(x.GetType())))
            {
                var t = tipo.GetProperty(propiedad.Name).GetValue(param, null);

                if (t == null || t.Equals(Constantes.ValoresPorDefecto[t.GetType()]))
                {
                    continue;
                }

                camposFiltro.Add(propiedad.Name, t);
            }

            return camposFiltro;
        }

    }
}
