using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace WcfServiceNlog
{
    public class ServiceModelTimeTakenEndpointBehavior : IEndpointBehavior
    {
        public ServiceModelTimeTakenEndpointBehavior()
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
            System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(
                new ServiceModelTimeTakenMessageInspector());
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }
        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }
        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}