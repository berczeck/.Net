using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace WcfServiceNlog
{
    public class ServiceModelTimeTakenMessageInspector : IDispatchMessageInspector
    {
        private class EnvoltorioMensaje
        {
            public string Request { get; set; }
            public string Response { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
            public string Usuario { private get; set; }
            public string Accion { private get; set; }

            public override string ToString()
            {
                return string.Format("Fecha Inicio:{6}{5}" +
                                     "Fecha Fin:{7}{5}" +
                                     "Duracion:{0}{5}" +
                                     "Usuario:{1}{5}" +
                                     "Accion:{2}{5}" +
                                     "Request:{5}{3}{5}" +
                                     "Response:{5}{4}{5}",
                    (int) new TimeSpan(FechaFin.Ticks - FechaInicio.Ticks).TotalSeconds, Usuario, Accion, Request,
                    Response, Environment.NewLine, FechaInicio.ToString("yy-MM-dd hh:mm:ss"),
                    FechaFin.ToString("yy-MM-dd hh:mm:ss"));
            }
        }

        public object AfterReceiveRequest(ref Message request,
            IClientChannel channel,
            InstanceContext instanceContext)
        {
            return new EnvoltorioMensaje
            {
                FechaInicio = DateTime.Now,
                Request = request.ToString(),
                Usuario = "admin",
                Accion = request.Headers.Action
            };
        }

        public void BeforeSendReply(ref Message reply,
            object correlationState)
        {
            var parametro = (EnvoltorioMensaje)correlationState;
            parametro.Response = reply.ToString();
            parametro.FechaFin = DateTime.Now;
            
            //Si demora mas de un tiempo X deberia enviar una notificacion
            CustomLogManager.Instancia.RegistrarTraza(parametro.ToString());
        }
    }
}