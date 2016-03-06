using System;
using System.Collections.Generic;
using System.Diagnostics;
using Framework.Error;
using NLog;

namespace Framework.Logging
{
    public class LogNet : ILogger
    {
        private readonly Logger _logger;
        private readonly IDictionary<NivelLog, LogLevel> _mapeoNivelError;

        public static INotificacionLog NotificacionExterna { get; set; }

        public LogNet()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _mapeoNivelError = new Dictionary<NivelLog, LogLevel>
            {
                {NivelLog.Debug, LogLevel.Debug},
                {NivelLog.Advertencia, LogLevel.Warn},
                {NivelLog.Error, LogLevel.Error},
                {NivelLog.Fatal, LogLevel.Fatal},
                {NivelLog.Informacion, LogLevel.Info},
                {NivelLog.Traza, LogLevel.Trace},
                {NivelLog.Otro, LogLevel.Info},
            };
        }

        public void RegistrarMensaje(string mensaje)
        {
            Registrar(NivelLog.Debug,
                new InformacionLog
                {
                    Mensaje = mensaje
                });
        }

        public void RegistrarError(InformacionLog informacionLog)
        {
            Registrar(NivelLog.Error, informacionLog);
        }

        public void RegistrarFatal(string mensaje)
        {
            Registrar(NivelLog.Fatal,
                new InformacionLog
                {
                    Mensaje = mensaje
                });
        }

        public void RegistrarFatal(InformacionLog informacionLog)
        {
            Registrar(NivelLog.Fatal, informacionLog);
        }

        public void RegistrarAdvertencia(string mensaje)
        {
            Registrar(NivelLog.Advertencia,
                new InformacionLog
                {
                    Mensaje = mensaje
                });
        }

        public void RegistrarAdvertencia(InformacionLog informacionLog)
        {
            Registrar(NivelLog.Advertencia, informacionLog);
        }

        public void RegistrarInformacion(string mensaje)
        {
            Registrar(NivelLog.Informacion,
                new InformacionLog
                {
                    Mensaje = mensaje
                });
        }

        public void RegistrarInformacion(InformacionLog informacionLog)
        {
            Registrar(NivelLog.Informacion, informacionLog);
        }

        public void RegistrarTraza(string mensaje)
        {
            Registrar(NivelLog.Traza,
                new InformacionLog
                {
                    Mensaje = mensaje
                });
        }

        public void RegistrarTraza(InformacionLog informacionLog)
        {
            Registrar(NivelLog.Traza, informacionLog);
        }

        public void RegistrarPersonalizado(InformacionLog informacionLog)
        {
            Registrar(NivelLog.Otro, informacionLog);
        }

        private void Registrar(NivelLog nivelLog, InformacionLog informacionLog)
        {
            LogLevel level = _mapeoNivelError[nivelLog];

            var theEvent =
                new LogEventInfo(level, string.Empty, informacionLog.Mensaje);

            if (informacionLog.Excepcion != null)
            {
                var detalleError = ExceptionHandler.Instancia.RecuperarDetalleError(informacionLog.Excepcion);
                theEvent.Properties["DatosError"] = detalleError;
            }

            var datosAdicionales = informacionLog.DatosAdicionales;

            if (datosAdicionales != null)
            {
                foreach (KeyValuePair<string, object> valor in datosAdicionales)
                {
                    theEvent.Properties[valor.Key] = datosAdicionales[valor.Key];
                }

                if (!datosAdicionales.ContainsKey("EventId"))
                {
                    theEvent.Properties["EventId"] = (Int16) nivelLog;
                }
            }

            var st = new StackTrace();
            StackFrame sf = st.GetFrame(2);

            var declaringType = sf.GetMethod().DeclaringType;
            if (declaringType != null)
            {
                theEvent.Properties["Modulo"] =
                    declaringType.Module.ScopeName;
                theEvent.Properties["Origen"] =
                    string.Format("{0}.{1}", declaringType.FullName, sf.GetMethod().Name);
            }
            
            _logger.Log(theEvent);
            
            if (NotificacionExterna != null)
            {
                NotificacionExterna.Registrar(nivelLog, informacionLog);
            }
        }
    }
}
