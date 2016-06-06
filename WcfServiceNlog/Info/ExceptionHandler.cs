using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WcfServiceNlog.Info
{
    public class ExcepxionHandler
    {
        private static readonly object SyncRoot = new Object();
        private static ExcepxionHandler _instancia;
        private static IDictionary<Type, dynamic> _map;

        private ExcepxionHandler()
        {
            _map = new Dictionary<Type, dynamic>
            {
                {typeof (Exception), new HandlerUnknowErrorInformation()},
                {typeof (SqlException), new HandlerSqlErrorInformation()}
            };
        }

        public static ExcepxionHandler Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new ExcepxionHandler();
                        }
                    }
                }

                return _instancia;
            }
        }

        public void AgregarExcepxion<T>(Type tipo, IHandlerErrorInformation<T> handler) where T : Exception
        {
            lock (((IDictionary) _map).SyncRoot)
            {
                if (!_map.ContainsKey(tipo))
                {
                    _map.Add(tipo, handler);
                }
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