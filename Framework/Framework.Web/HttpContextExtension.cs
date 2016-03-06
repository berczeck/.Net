using System.Globalization;
using System.IO;
using System.Web;
using Framework.Web.Export;

namespace Framework.Web
{
    public static class HttpContextExtension
    {
        public static void DescargarArchivo(this HttpContext context, string contenido, string nombreArchivo,
            string contentType = null)
        {
            contentType = string.IsNullOrWhiteSpace(contentType) ? MimeType.NoConocido : contentType;
            Descargar(context, nombreArchivo, contentType, contenido);
        }

        public static void DescargarArchivo(this HttpContext context, byte[] contenido, string nombreArchivo,
            string contentType = null)
        {
            contentType = string.IsNullOrWhiteSpace(contentType) ? MimeType.NoConocido : contentType;
            Descargar(context, nombreArchivo, contentType, contenidoBytes: contenido);
        }

        public static void Descargar(HttpContext context,  string nombreArchivo, string contentType,string contenido = null, byte[] contenidoBytes=null)
        {
            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", nombreArchivo));
            // set content type
            context.Response.ContentType = contentType;

            if (!string.IsNullOrWhiteSpace(contenido))
            {
                // add headers
                context.Response.AddHeader("Content-Length", contenido.Length.ToString(CultureInfo.InvariantCulture));
                // do AppendLine binary data to responce stream
                context.Response.Write(contenido);
            }

            if (contenidoBytes != null)
            {
                // add headers
                context.Response.AddHeader("Content-Length", contenidoBytes.Length.ToString(CultureInfo.InvariantCulture));
                MemoryStream ms = new MemoryStream(contenidoBytes);
                context.Response.Buffer = true;
                ms.WriteTo(context.Response.OutputStream);
            }

            // finish process
            context.Response.Flush();
            context.Response.SuppressContent = true;
            context.ApplicationInstance.CompleteRequest();
        }
    }
}
