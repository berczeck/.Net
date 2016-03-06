using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Framework.Error
{
    public class ExceptionHandler
    {
        private static readonly object SyncRoot = new Object();
        private static ExceptionHandler _instancia;
        private static IDictionary<Type, IHandlerErrorInformation> _map;

        private ExceptionHandler()
        {
            _map = new Dictionary<Type, IHandlerErrorInformation>
            {
                {typeof (Exception), new HandlerUnknowErrorInformation()},
                {typeof (SqlException), new HandlerSqlErrorInformation()}
            };
        }

        public static ExceptionHandler Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new ExceptionHandler();
                        }
                    }
                }

                return _instancia;
            }
        }

        public void AgregarExcepxion(Type tipo, IHandlerErrorInformation handler)
        {
            if (!_map.ContainsKey(tipo))
            {
                _map.Add(tipo, handler);
            }
        }

        private void DetalleError(Exception exception, MessageFormat format)
        {
            Type tipo = exception.GetType();
            var handler = _map.ContainsKey(tipo) ? _map[tipo] : _map[typeof (Exception)];

            handler.AddExceptionInformation(format, exception);
        }

        public void RecuperarDetalleError(Exception exception, StringBuilder mensajeError)
        {
            DetalleError(exception, new MessageFormat(mensajeError));
        }
        
        public string RecuperarDetalleError(Exception exception)
        {
            var format = new MessageFormat();
            DetalleError(exception, format);

            return format.Message;
        }
    }
}