using System;
using System.IO;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Framework.Error;

namespace Framework.Wcf.Error
{
    public class ErrorMessageInspector : IClientMessageInspector
    {
         private readonly bool _enabled;

         public ErrorMessageInspector(bool enabled)
        {
            _enabled = enabled;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (!reply.IsFault || !_enabled) return;
            
            MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
            Message copy = buffer.CreateMessage();
            reply = buffer.CreateMessage();

            ErrorServicioRespuesta exception = RecuperarFaultException(copy);
            
            if (exception == null) return;

            switch (exception.Tipo)
            {
                case TipoErrorServicio.ErrorValidacion:
                    throw new ExcepcionValidacion(exception.Mensaje);
                case TipoErrorServicio.ErrorNegocio:
                    throw new ExcepcionReglaNegocio(exception.Codigo, exception.Mensaje);
                default:
                    throw new ExcepcionServicio(
                        "La respuesta no pudo ser atendida por el servicio. Por favor intente más tarde.");
            }
        }

        public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {
            return null;
        }

        private static ErrorServicioRespuesta RecuperarFaultException(Message reply)
        {
            const string detailElementName = "detail";

            using (XmlDictionaryReader reader = reply.GetReaderAtBodyContents())
            {
                // Buscar <soap:Detail>
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.LocalName.ToLower().Equals(detailElementName))
                    {
                        break;
                    }
                }

                // Se encontro el elemento?
                if (reader.NodeType != XmlNodeType.Element || reader.LocalName != detailElementName)
                {
                    return null;
                }

                // Hay algun elemento? <soap:Detail>
                if (!reader.Read())
                {
                    return null;
                }

                var xml = new XmlDocument();
                xml.Load(reader);

                //Elimina el namespace xlmns, el cual no permite deserializar el objeto
                xml = EliminarXmlns(xml);

                using (var xmlReader = XmlReader.Create(new StringReader(xml.InnerXml)))
                {
                    xmlReader.MoveToContent();
                    switch (xmlReader.Name)
                    {
                        case "ErrorServicioRespuesta":
                            return new XmlSerializer(typeof(ErrorServicioRespuesta)).Deserialize(xmlReader) as ErrorServicioRespuesta;
                        default:
                            throw new NotSupportedException("Elemento inesperado: " + reader.Name);
                    }
                }
            }
        }

        private static XmlDocument EliminarXmlns(XmlDocument doc)
        {
            XDocument d;
            using (var nodeReader = new XmlNodeReader(doc))
                d = XDocument.Load(nodeReader);

            while (d.Root != null && d.Root.HasAttributes)
            {
                d.Root.FirstAttribute.Remove();
            }

            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            var xmlDocument = new XmlDocument();
            using (var xmlReader = d.CreateReader())
                xmlDocument.Load(xmlReader);

            return xmlDocument;
        }

    }
}
