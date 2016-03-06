using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Framework.Wcf.Traza
{
    public class Behavior : IEndpointBehavior
    {
        public Behavior()
        {
            Enabled = true;
        }

        internal Behavior(bool enabled)
        {
            Enabled = enabled;
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new InspectorCliente(Enabled));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new InspectorServicio(Enabled));
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        public bool Enabled { get; set; }
    }
}