using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReactDemo.Controllers
{
    [HandleError]
    public class ExceptionsController : Controller
    {
        // GET: Exceptions
        public ActionResult Index()
        {
            throw new Exception("Miramee");
            return View();
        }
    }
}