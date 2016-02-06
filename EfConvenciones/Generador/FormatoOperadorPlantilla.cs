using System;
using System.Collections.Generic;
using System.Linq;

namespace EfConvenciones.Generador
{
    public abstract class FormatoOperadorPlantilla : IFormatoOperadorQuery
    {
        public IInspectorPropiedades Inspector { get; set; }

        public virtual ResultadoBusqueda GenerarQuery<T>(T entidad, int indice = 0)
        {
            var listaFiltro = new List<string>();
            var parametros = new List<object>();

            var camposFiltro = Inspector.RecuperarCamposActualizados(entidad);

            camposFiltro.OrderBy(x => x.Key).Select(x => x.Key).ToList().ForEach(x =>
            {
                parametros.Add(camposFiltro[x]);
                listaFiltro.Add(string.Format("{0} = @{1}", x, indice));
                indice++;
            });

            return new ResultadoBusqueda
            {
                Filtro =
                    listaFiltro.Any()
                        ? string.Format("({0})", listaFiltro.Aggregate((a, b) => a + " " + Operador + " " + b))
                        : string.Empty,
                Parametros = parametros
            };
        }

        public virtual ResultadoBusqueda GenerarQuery<T>(ResultadoBusqueda resultado, T entidad)
        {
            var resultadoActual = GenerarQuery(entidad, resultado.Parametros.Count);

            var parametros = new object[resultadoActual.Parametros.Count + resultado.Parametros.Count];
            resultado.Parametros.CopyTo(parametros, 0);
            resultadoActual.Parametros.CopyTo(parametros, resultado.Parametros.Count);

            return new ResultadoBusqueda
            {
                Filtro = string.Format("({0} {1} {2})", resultado.Filtro, Operador, resultadoActual.Filtro),
                Parametros = parametros.ToList()
            };
        }

        protected abstract string Operador { get; }
    }
}
