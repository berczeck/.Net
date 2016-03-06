using System;
using Framework.Error;

namespace Implementacion.Error
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //1. Referenciar el ensamblado Framework
            //2. Referenciar el ensamblado NLog
            //3. Importar el namespace Framework.Error
            //4. Agregar el archivo Nlog.config y actualizar el formato

            //Ejemplo: como agregar controladores de excepcion personalizados
            ExceptionHandler.Instancia.AgregarExcepxion(typeof (ExcepcionReglaNegocio),
                new HandlerReglaNegocioErrorInformation());

            try
            {
                throw new ExcepcionReglaNegocio("RN0004","Codigo incorrecto");
            }
            catch (Exception ex)
            {
               string detalle =  ExceptionHandler.Instancia.RecuperarDetalleError(ex);

                Console.WriteLine(detalle);
            }

            try
            {
                String valor = null;
                valor.Substring(0, 3);
            }
            catch (Exception ex)
            {
                string detalle = ExceptionHandler.Instancia.RecuperarDetalleError(ex);

                Console.WriteLine(detalle);
            }

            Console.ReadLine();
        }
    }

    public class HandlerReglaNegocioErrorInformation : IHandlerErrorInformation
    {
        public void AddExceptionInformation(MessageFormat messageFormat, 
            Exception excepcion)
        {
            var error = (ExcepcionReglaNegocio) excepcion;

            messageFormat.AddExtraInformation("Tipo","Error de prueba");
            messageFormat.AddExtraInformation("Cantidad de errores", "8");
            messageFormat.AddExtraInformation("Duracion", "48");
            messageFormat.AddExtraInformation("Codigo", error.Codigo);
            messageFormat.AddExtraInformation("Mensajes", error.Message);
        }
    }
}
