using System;
using NLog;

namespace WcfServiceNlog
{

    public class CustomLogManager 
    {
         //LogManager.GetCurrentClassLogger(); Hace referencia a las clases con mobre "*"
        //LogManager.GetLogger("Example"); Trae una traza por el nombre
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static Logger _logger;
        private static readonly object SyncRoot = new Object();
        private static CustomLogManager _instancia;

        private CustomLogManager()
        {
            if (_logger == null)
            {
                _logger = LogManager.GetCurrentClassLogger();
            }
        }

        public static CustomLogManager Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new CustomLogManager();
                        }
                    }
                }

                return _instancia;
            }
        }

        public void RegistrarMensaje(string mensaje)
        {
            _logger.Debug(mensaje);
        }

        public void RegistrarMensaje(string mensaje, Exception exception)
        {
            _logger.DebugException(mensaje, exception);
        }

        public void RegistrarError(string mensaje)
        {
            _logger.Error(mensaje);
        }

        public void RegistrarError(string mensaje, Exception exception)
        {
            _logger.ErrorException(mensaje, exception);
        }

        public void RegistrarFatal(string mensaje)
        {
            _logger.Fatal(mensaje);
        }

        public void RegistrarFatal(string mensaje, Exception exception)
        {
            _logger.FatalException(mensaje, exception);
        }

        public void RegistrarAdvertencia(string mensaje)
        {
            _logger.Warn(mensaje);
        }

        public void RegistrarAdvertencia(string mensaje, Exception exception)
        {
            _logger.WarnException(mensaje, exception);
        }

        public void RegistrarInformacion(string mensaje)
        {
            _logger.Info(mensaje);
        }

        public void RegistrarInformacion(string mensaje, Exception exception)
        {
            _logger.InfoException(mensaje, exception);
        }

        public void RegistrarTraza(string mensaje)
        {
            _logger.Trace(mensaje);
        }

        public void RegistrarTraza(string mensaje, Exception exception)
        {
            _logger.TraceException(mensaje, exception);
        }

    }
}