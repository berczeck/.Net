using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Framework.Wcf
{
    public static class OperationContextHelper
    {
        public static string ObtnerIp(this OperationContext contexto)
        {
            if (contexto == null)
            {
                return string.Empty;
            }

            RemoteEndpointMessageProperty endpointProperty = null;

            try
            {
                MessageProperties messageProperties = contexto.IncomingMessageProperties;
                endpointProperty =
                    messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            }
            catch
            {
                //Solo ocurre cuando el mensaje esta cerrado, no se necesita registrar este tipo de error.
            }

            return endpointProperty != null
                ? string.Format("{0}:{1}", endpointProperty.Address, endpointProperty.Port)
                : string.Empty;
        }
    }
}
