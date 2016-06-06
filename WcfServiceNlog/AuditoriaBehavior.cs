
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Web;

namespace WcfServiceNlog
{
    public class AuditoriaBehaviorAttribute : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription,
            System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(OperationDescription operationDescription,
            System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
            clientOperation.ParameterInspectors.Add(new AuditoriaParametro());
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription,
            System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new AuditoriaParametro());
        }

        public void Validate(OperationDescription operationDescription)
        {

        }
    }
}