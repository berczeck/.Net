using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace WcfServiceNlog
{
    public class ServiceModelTimeTakenBehaviorExtensionElement: BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new ServiceModelTimeTakenEndpointBehavior();
        }
        public override Type BehaviorType
        {
            get { return typeof(ServiceModelTimeTakenEndpointBehavior); }
        }

    }
}