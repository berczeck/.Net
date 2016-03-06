using System;
using System.Collections.Generic;

namespace Framework.Wcf.Traza
{
    internal class EnvoltorioMensaje
    {
        public string Identificador { get; set; }

        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Accion { get; set; }
        public string Ip { get; set; }
        public Dictionary<string, string> DatosAdicionales { get; set; }

        public int Duracion
        {
            get
            {
                return (int) new TimeSpan(FechaFin.Ticks - FechaInicio.Ticks).TotalSeconds;
            }
        }

        public EnvoltorioMensaje()
        {
            Identificador = Guid.NewGuid().ToString();
        }

        public IDictionary<string, object> InformacionAdicional()
        {
            return new Dictionary<string, object>
            {
                {"Identificador", Identificador},
                {"Accion", Accion},
                {"FechaInicio", FechaInicio},
                {"FechaFin", FechaFin},
                {"Duracion", Duracion},
                {"Request", Request},
                {"Response", Response},
                {"Ip", Ip},
            };
        }
    }
}
