using System;
using System.Runtime.Serialization;

namespace Framework.Wcf.Error
{
    [DataContract]
    public class ErrorServicioRespuesta
    {
        public ErrorServicioRespuesta()
        {
            Fecha = DateTime.Now;
        }

        [DataMember]
        public TipoErrorServicio Tipo { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public string Codigo { get; set; }
    }
}