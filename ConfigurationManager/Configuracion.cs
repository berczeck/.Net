using System.Collections.Generic;

namespace ConfigurationManager
{
    public class Configuracion
    {
        public string Nombre { get; set; }
        public string Mensaje { get; set; }
        public List<int> Valores { get; set; }

        public Dictionary<string,string> Llaves { get; set; }
        public Datos Datos { get; set; }

    }

    public class Datos
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
    }
}
