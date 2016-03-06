using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace Framework.Wcf.Error
{
    public class BehaviorErrorTransformationSection : BehaviorExtensionElement
    {
        private const string HabilitadoAttributeName = "habilitado";

        [ConfigurationProperty(HabilitadoAttributeName, DefaultValue = true, IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)base[HabilitadoAttributeName]; }
            set { base[HabilitadoAttributeName] = value; }
        }

        public override Type BehaviorType
        {
            get { return typeof(BehaviorErrorTransformation); }
        }

        protected override object CreateBehavior()
        {
            return new BehaviorErrorTransformation(Enabled);
        }
    }
}
