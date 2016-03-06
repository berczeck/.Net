using System;
using Framework.Logging;

namespace Implementacion.Logging
{
    public class NotificacionLog : INotificacionLog
    {
        public void Registrar(NivelLog nivelLog, InformacionLog informacionLog)
        {
            Console.WriteLine(informacionLog.Excepcion.Message);
        }
    }
}
