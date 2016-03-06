using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace Framework.Wcf.Error
{
    public class BehaviorSection : BehaviorExtensionElement
    {
        private const string HabilitadoAttributeName = "habilitado";

        [ConfigurationProperty(HabilitadoAttributeName, DefaultValue = true, IsRequired = true)]
        public bool Habilitado
        {
            get { return (bool) base[HabilitadoAttributeName]; }
            set { base[HabilitadoAttributeName] = value; }
        }

        public override Type BehaviorType
        {
            get { return typeof (Behavior); }
        }

        protected override object CreateBehavior()
        {
            return new Behavior(Habilitado);
        }
    }
}