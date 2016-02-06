using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PaginacionKo.Util;

namespace PaginacionKo.Controllers
{
    public class ActivarController : ApiController
    {
        [HttpPost]
        [ActionName("producto")]
        public HttpResponseMessage PostProducto(int[] ids)
        {
            Repositorio.ListaProductos.Where(x => ids.Contains(x.Id)).ToList().ForEach(x => x.Active = false);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ActionName("material")]
        public HttpResponseMessage Material(int[] ids)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
