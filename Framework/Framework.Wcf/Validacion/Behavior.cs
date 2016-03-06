using System;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Framework.Wcf.Validacion
{
    public class Behavior : Attribute, IServiceBehavior
    {
        public Behavior()
        {
            Enabled = true;
        }

        internal Behavior(bool enabled)
        {
            Enabled = enabled;
        }
        public bool Enabled { get; set; }
        
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            if (!Enabled)
            {
                return;
            }

            var operations =
                from dispatcher in serviceHostBase.ChannelDispatchers.Cast<ChannelDispatcher>()
                from endpoint in dispatcher.Endpoints
                from operation in endpoint.DispatchRuntime.Operations
                select operation;

            operations.ToList()
                .ForEach(operation => operation.ParameterInspectors.Add(new ValidadorNotacionInspector()));
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }
    }
}
