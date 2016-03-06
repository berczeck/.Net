using System;
using System.ServiceModel.Description;

namespace Framework.Wcf.Validacion
{
    public class ValidadorNotacionAttribute : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
            clientOperation.ParameterInspectors.Add(new ValidadorNotacionInspector());
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new ValidadorNotacionInspector());
        }

        public void Validate(OperationDescription operationDescription)
        {
        }
    }
}
