using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Framework.Error;

namespace Framework.Wcf.Error
{
    public class Handler : IErrorHandler
    {
        public string IpRequest { get; set; }
        public Handler()
        {
            IpRequest = OperationContext.Current.ObtnerIp();
        }
        
        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            if (error is FaultException<ErrorServicioRespuesta>) return true;

            ErrorManager.RegistrarError(error, new Dictionary<string, object>
                {
                    {"Identificador", Guid.NewGuid().ToString()},
                    {"Ip", IpRequest},
                    {"Tipo", error.GetType().Name}
                });

            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message msg)
        {
            if (error is FaultException<ErrorServicioRespuesta>)
                return;

            ErrorServicioRespuesta faultDetail = ErrorBuilder.ConstruirError(error);

            if (faultDetail == null) return;

            Type faultType =
                typeof (FaultException<>).MakeGenericType(faultDetail.GetType());

            var faultReason = new FaultReason(faultDetail.Mensaje);
            FaultCode faultCode = FaultCode.CreateReceiverFaultCode(new FaultCode(faultDetail.Codigo));

            var faultException =
                (FaultException) Activator.CreateInstance(faultType, faultDetail, faultReason, faultCode);

            MessageFault faultMessage = faultException.CreateMessageFault();
            
            msg = Message.CreateMessage(version, faultMessage, faultException.Action);
        }

        #endregion
        public string NombreServicio { get; set; }
    }
}