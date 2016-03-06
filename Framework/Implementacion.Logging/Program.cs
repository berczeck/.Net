using System;
using System.Collections.Generic;
using System.ServiceModel;
using Framework.Logging;

namespace Implementacion.Logging
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //1. Referenciar el ensamblado Framework
            //2. Referenciar el ensamblado NLog
            //3. Importar el namespace Framework.Logging
            //4. Agregar el archivo Nlog.config y actualizar el formato


            //Usando el objeto mediante una Fachada
            CustomLogManager.Instancia.RegistrarFatal(new InformacionLog
            {
                Excepcion = new ArgumentNullException("Error fatal"),
                Mensaje = "Error fatal de prueba",
                DatosAdicionales = //Parametro opcional
                new Dictionary<string, object> {{"Tipo", "Fatal"}, {"Fuente", "Demo"}}
            });

            CustomLogManager.Instancia.RegistrarError(new InformacionLog
            {
                Excepcion = new ArgumentNullException("Error"),
                Mensaje = "Error de prueba",
                DatosAdicionales = //Parametro opcional
                new Dictionary<string, object> { { "Comando", "Error" }, { "Fuente", "Demo" } }
            });

            CustomLogManager.Instancia.RegistrarAdvertencia(new InformacionLog
            {
                Mensaje = "Advertencia de prueba",
                DatosAdicionales =//Parametro opcional
                new Dictionary<string, object> { { "Tiempo", "20" }, { "Usuario", "admin" } }
            });

            CustomLogManager.Instancia.RegistrarInformacion(new InformacionLog
            {
                Mensaje = "Informacion de prueba",
                DatosAdicionales =//Parametro opcional
                new Dictionary<string, object> { { "Usuario", "admin" } }
            });

            CustomLogManager.Instancia.RegistrarTraza(new InformacionLog
            {
                Mensaje = "Traza de prueba",
                DatosAdicionales =//Parametro opcional
                new Dictionary<string, object> { { "Request", "<xmlRequest>" }, { "Response", "<xmlResponse>" } }
            });

            CustomLogManager.Instancia.RegistrarMensaje("Mensaje de prueba");

            //Llamando directo al objeto, para escenarios de DI
            ILogger logger = new LogNet();

            logger.RegistrarMensaje("Mensaje personalizado");
            logger.RegistrarError(new InformacionLog
            {
                Excepcion = new ArgumentNullException("Error"),
                Mensaje = "Error de prueba",
                DatosAdicionales = //Parametro opcional
                new Dictionary<string, object> { { "Comando", "Error" }, { "Fuente", "Demo" } }
            });

            //Capturando las nofiticaciones del log

            LogNet.NotificacionExterna = new NotificacionLog();

            try
            {
                throw new CommunicationException("Error de conexion al servicio de prueba");
            }
            catch (Exception ex)
            {
                CustomLogManager.Instancia.RegistrarFatal(new InformacionLog
                {
                    Excepcion = ex,
                    Mensaje = "Error fatal de prueba",
                    DatosAdicionales = //Parametro opcional
                    new Dictionary<string, object> { { "Tipo", "Fatal" }, { "Fuente", "Demo" } }
                });
            }

            Console.Read();
        }
    }
}
