using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SiteCaching.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public int TipoSexo { get; set; }

        public string Email { get; set; }

        public List<Pedido> Pedidos { get; set; }
    }

    [Serializable]
    public class PersonaCustomizada : Persona, ISerializable
    {
        public PersonaCustomizada()
        {
            
        }
        public PersonaCustomizada(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetInt32("Id");
            Nombre = info.GetString("Nombre");
            ApellidoMaterno = info.GetString("ApellidoMaterno");
            ApellidoPaterno = info.GetString("ApellidoPaterno");
        }

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Nombre", Nombre);
            info.AddValue("ApellidoPaterno", ApellidoPaterno);
            info.AddValue("ApellidoMaterno", ApellidoMaterno);
        }

        #endregion
    }
}