using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using Framework.Logging;

namespace Framework.Error
{
    public static class ErrorManager
    {
        public const Int16 CodigoErrorBaseDatos = 200;
        public const Int16 CodigoErrorComunicacion = 100;

        private static readonly int[] ListaErrores =
        {
            //Cannot open database requested by the login. The login failed.
            4060,
            //Login failed for user
            18456,18452,
            //A network-related or instance-specific error occurred while establishing a connection to SQL Server.
            53,
            2
        };

        private static bool ErrorEsFatal(Exception error, out Int16 codigo, out string mensaje )
        {
            mensaje =
                   string.Format("Ocurrió un error fatal en la aplicación del tipo {0} con el mensaje {1}, para poder resolver el problema revise los errores registrados en el visor de eventos del servidor {2}.\r\n\r\n", error.GetType(), error.Message, Environment.MachineName);

            codigo = CodigoErrorBaseDatos;
            Type tipoError = error.GetType();
            if (tipoError == typeof(CommunicationException) || tipoError == typeof(EndpointNotFoundException))
            {
                codigo = CodigoErrorComunicacion;
                mensaje +=
                    "Este error puede ser producido por uno de los siguientes motivos:\r\n\r\n1. El sistema tiene un problema de comunicación con un servicio web.\r\n2. El sistema no se puede conectar con un  servicio web.";
                return true;
            }
            var errorBd = (error as SqlException);
            mensaje +=
                "Este error puede ser producido por uno de los siguientes motivos:\r\n\r\n1. La base de datos no existe.\r\n2. Cuando no se pudo iniciar sesión.\r\n3. No se pudo establecer una conexión al servidor Sql.";
            return errorBd != null && ListaErrores.Contains(errorBd.Number);
        }

        public static void RegistrarError(Exception error)
        {
            RegistrarError(error,new Dictionary<string, object>());
        }

        public static void RegistrarError(Exception error, Dictionary<string, object> datosAdicionales)
        {
            var informacion = new InformacionLog
            {
                DatosAdicionales = datosAdicionales
            };

            Int16 codigoError;
            string mensaje;
            //Registrar Mensajes
            if (ErrorEsFatal(error, out codigoError, out mensaje))
            {
                informacion.Mensaje = mensaje;
                informacion.Excepcion = error;

                informacion.DatosAdicionales.Add("EventId", codigoError);

                CustomLogManager.Instancia.RegistrarFatal(informacion);
            }
            else
            {
                if (error is ExcepcionReglaNegocio)
                {
                    informacion.Mensaje = string.Format("Regla de negocio : {0}", error.Message);
                    CustomLogManager.Instancia.RegistrarInformacion(informacion);
                }
                else if (error is ExcepcionValidacion)
                {
                    informacion.Mensaje = string.Format("Regla de Validaciòn : {0}", error.Message);
                    CustomLogManager.Instancia.RegistrarInformacion(informacion);
                }
                else
                {
                    informacion.Mensaje = "Error no controlado.";
                    informacion.Excepcion = error;

                    CustomLogManager.Instancia.RegistrarError(informacion);
                }
            }
        }
    }
}
