
namespace Framework.Logging
{
    public class CustomLogManager 
    {
        private static ILogger _logger;
        private static readonly object SyncRoot = new object();
        private static CustomLogManager _instancia;

        private CustomLogManager()
        {
            if (_logger == null)
            {
                _logger = LogBuilder.CurrentClassLogger;
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

        
        public LogServiceLocator LogBuilder
        {
            get
            {
                return new LogServiceLocator();
            }
        }

        /// <summary>
        /// Registar un mensaje con fines de depuración
        /// </summary>
        public void RegistrarMensaje(string mensaje)
        {
            _logger.RegistrarMensaje(mensaje);
        }

        /// <summary>
        /// Registar una excepción pero no impide el buen funcionamiento del aplicativo
        /// </summary>
        public void RegistrarError(InformacionLog informacionLog)
        {
            _logger.RegistrarError(informacionLog);
        }

        /// <summary>
        /// Registar una excepción que impida que funcione correctamente el aplicativo, por ejemplo: 
        /// <para>Error al conectarse a la base de datos</para>
        /// <para>Error al conectarse a un servicio </para>
        /// </summary>
        public void RegistrarFatal(InformacionLog informacionLog)
        {
            _logger.RegistrarFatal(informacionLog);
        }
        
        /// <summary>
        /// Registar un mensaje de advertencia, por ejemplo: 
        /// <para>Cuando un request toma mucho tiempo</para>
        /// <para>Cuando hay un comportamiento extraño en el aplicativo</para>
        /// </summary>
        public void RegistrarAdvertencia(InformacionLog informacionLog)
        {
            _logger.RegistrarAdvertencia(informacionLog);
        }
        
        /// <summary>
        /// Registar un mensaje de advertencia, por ejemplo: 
        /// <para>Cuando se  dispara una regla de negocio</para>
        /// </summary>
        public void RegistrarInformacion(InformacionLog informacionLog)
        {
            _logger.RegistrarInformacion(informacionLog);
        }


        /// <summary>
        /// Registar la traza de una solicitud, por ejemplo:
        /// <para>Mensajes de entrada y salida de un servicio</para>
        /// <para>El contenido de un objeto serializado</para>
        /// </summary>
        public void RegistrarTraza(InformacionLog informacionLog)
        {
            _logger.RegistrarTraza(informacionLog);
        }

    }
}