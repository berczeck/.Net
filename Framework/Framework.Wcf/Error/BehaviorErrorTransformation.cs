using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Framework.Wcf.Error
{
    public class BehaviorErrorTransformation: IEndpointBehavior
    {
        public BehaviorErrorTransformation()
        {
            Enabled = true;
        }

        internal BehaviorErrorTransformation(bool enabled)
        {
            Enabled = enabled;
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ErrorMessageInspector(Enabled));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        public bool Enabled { get; set; }
    }
}
