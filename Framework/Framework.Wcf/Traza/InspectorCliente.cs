using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Framework.Comun;
using Framework.Logging;

namespace Framework.Wcf.Traza
{
    public class InspectorCliente : IClientMessageInspector
    {
        private readonly bool _enabled;

        public InspectorCliente(bool enabled)
        {
            _enabled = enabled;
        }

        public string NombreServicio { get; set; }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var parametro = (EnvoltorioMensaje) correlationState;
            if (!reply.IsFault && !string.IsNullOrEmpty(reply.ToString()) && correlationState != null)
            {
                parametro.Response = reply.ToString();
            }

            parametro.FechaFin = DateTime.Now;

            if (_enabled)
            {
                CustomLogManager.Instancia.RegistrarTraza(new InformacionLog
                {
                    Mensaje = "Traza de mensajes Servicio Externo",
                    DatosAdicionales = parametro.InformacionAdicional()
                });
            }

            //Si la llamada demora mas del tiempo configurado se envia a una tarza especial
            int tiempoSegundosLimiteLlamada = HelperConfig.GetInt32("Wcf:InspectorCliente:LimiteTiempoLLamada");

            if (tiempoSegundosLimiteLlamada <= 0 || tiempoSegundosLimiteLlamada >= parametro.Duracion)
            {
                return;
            }

            string mensaje =
                string.Format(
                    "Tiempo Limite por Llamada excedido(max {0} seg) {4}Duracion: {1} seg {4}Accion : {2}{4}[Request]{4}{3}{4}[Response]{4}{5}",
                    tiempoSegundosLimiteLlamada, parametro.Duracion, parametro.Accion,
                    parametro.Request, Environment.NewLine, parametro.Response);
            
            CustomLogManager.Instancia.RegistrarAdvertencia(
                new InformacionLog
                {
                    Mensaje = mensaje,
                    DatosAdicionales = new Dictionary<string, object>
                    {
                        {"Identificador", parametro.Identificador},
                        {"Ip", parametro.Ip},
                        {"Tipo", "Llamada Larga Externa"}
                    }
                }
                );
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (string.IsNullOrEmpty(request.ToString()))
            {
                return null;
            }

            var credenciales = new EnvoltorioMensaje
            {
                FechaInicio = DateTime.Now,
                Request = request.ToString(),
                Accion = request.Headers.Action,
                Ip = OperationContext.Current.ObtnerIp()
            };
            
            return credenciales;
        }
    }
}