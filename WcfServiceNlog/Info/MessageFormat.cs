using System.Text;

namespace WcfServiceNlog.Info
{
    public class MessageFormat
    {
        private readonly StringBuilder _datosAdicionales;
        public MessageFormat()
        {
            _datosAdicionales = new StringBuilder();
        }

        public MessageFormat(StringBuilder datosAdicionales)
        {
            _datosAdicionales = datosAdicionales;
        }

        public void AddExtraInformation(string nombre, string valor)
        {
            _datosAdicionales.Append(string.Format("{0}=\"{1}\"\r\n", nombre, valor));
        }

        public string Message {
            get
            {
                return _datosAdicionales.ToString();
            }
        }
    }
}