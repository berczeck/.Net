using System;
using System.Xml;
using System.Xml.Linq;

namespace RemoverNamespaceXlm
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(@"<ErrorServicioRespuesta xmlns='http://schemas.datacontract.org/2004/07/Ig.Framework.Wcf.Error' xmlns:i='http://www.w3.org/2001/XMLSchema-instance'><Codigo>0000</Codigo></ErrorServicioRespuesta>");

            xml = RemoveXmlns(xml);

            string final = xml.InnerXml;

            Console.WriteLine(final);

            Console.ReadLine();
        }


        public static XmlDocument RemoveXmlns(XmlDocument doc)
        {
            XDocument d;
            using (var nodeReader = new XmlNodeReader(doc))
                d = XDocument.Load(nodeReader);

            while (d.Root.HasAttributes)
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
