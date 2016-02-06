using System;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using MvcStreamingFile.App_Start;

namespace MvcStreamingFile.Controllers
{
    public class ControllerStreamingConfigAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings settings,
            HttpControllerDescriptor descriptor)
        {
            // Add a custom action selector.
            settings.Services.Replace(typeof (IHostBufferPolicySelector), new NoBufferPolicySelector());
        }
    }
}