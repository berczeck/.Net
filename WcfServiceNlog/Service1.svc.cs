using System;
using WcfServiceNlog.Info;

namespace WcfServiceNlog
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void SetLog(string mensaje)
        {
            CustomLogManager.Instancia.RegistrarMensaje(mensaje);
        }

        public string GetData(int value)
        {
            CustomLogManager.Instancia.RegistrarMensaje(value.ToString());

            CustomLogManager.Instancia.RegistrarInformacion(string.Format("advertencia a las {0}",
                DateTime.Now.ToString("yyyyMMddhhmmss")));

            try
            {
                string n = null;
                n = n.Substring(1);
            }
            catch (Exception ex)
            {
                var mensaje = ExcepxionHandler.Instancia.RecuperarDetalleError(ex);
                CustomLogManager.Instancia.RegistrarError(mensaje, ex);
            }

            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
