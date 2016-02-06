using System;
using System.Web.Http;

namespace PaginacionKo.Controllers
{
    public class BandejaController : ApiController
    {
        //ref: http://www.asp.net/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api

        //Post<Accion> == HttpPost
        //Get<Accion> == HttpGet
        //Put<Accion> == HttpPut
        //Delete<Accion> == HttpDelete

        //Producto?producto=pro
        //paginarProductos?producto=pro
        [HttpGet]
        [ActionName("paginarProductos")]
        public string Producto(string producto)
        {
            return string.Format("Producto {0} - {1}", DateTime.Now, producto);
        }

        public string GetPersona(string persona)
        {
            return string.Format("Persona {0} - {1}", DateTime.Now, persona);
        }

        [NonAction]
        private void Algo()
        {
        }
    }
}