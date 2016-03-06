using System;
using System.Collections.Generic;

namespace Framework.Logging
{
    public class InformacionLog
    {
        public string Mensaje { get; set; }
        public Exception Excepcion { get; set; }
        public IDictionary<string, object> DatosAdicionales { get; set; }
        
        /// <summary>
        /// Solo se usa cuando la propiedad NivelLog =  NivelLog.Otro
        /// </summary>
        public string NivelLogPersonalizado { get; set; }
    }
}
