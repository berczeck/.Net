using System.Net.Http;
using System.Web.Http.WebHost;

namespace MvcStreamingFile.App_Start
{
    public class NoBufferPolicySelector : WebHostBufferPolicySelector
    {
        public override bool UseBufferedInputStream(object hostContext)
        {
            return false;
        }

        public override bool UseBufferedOutputStream(HttpResponseMessage response)
        {
            return false;
        }
    }
}