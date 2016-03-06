using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReactDemo
{
    using System.Diagnostics;

    public class UtplCustomHandlerError : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var ex = filterContext.Exception;

            if (ex == null) return;
            
            //Register error

            Debug.WriteLine(ex);
        }
    }

    public class IGCustomHandlerError : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var ex = filterContext.Exception;

            if (ex == null) return;

            //Register error

            Debug.WriteLine(ex);
        }
    }
}