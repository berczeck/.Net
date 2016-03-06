using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Framework.Wcf.Error
{
    public class Behavior : IServiceBehavior
    {
        internal Behavior(bool enabled)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; set; }

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (Enabled)
            {
                foreach (ChannelDispatcherBase chanDispBase in serviceHostBase.ChannelDispatchers)
                {
                    var channelDispatcher = chanDispBase as ChannelDispatcher;
                    if (channelDispatcher == null)
                        continue;
                    channelDispatcher.ErrorHandlers.Add(new Handler());
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}