using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PaginacionKo.Models;
using PaginacionKo.Util;

namespace PaginacionKo.Controllers
{
    public class ProductoController : ApiController
    {
        public IEnumerable<Producto> Get()
        {
            return Repositorio.ListaProductos;
        }

        public HttpResponseMessage Post(int id)
        {
            GetPorId(id).Active = false;

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public class RequestProducto
        {
            public int Id { get; set; }
        }

        public HttpResponseMessage PostBody([FromBody]RequestProducto req)
        {
            GetPorId(req.Id).Active = false;

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        [HttpPut]
        public HttpResponseMessage Actualizarestado(int id)
        {
            GetPorId(id).Active = false;

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public Producto GetPorId(int id)
        {
            return Repositorio.ListaProductos.FirstOrDefault(x => x.Id==id);
        }
        
        public HttpResponseMessage GetPaginado(int pagina, int tamanio, string nombre)
        {
            return GetResultado(pagina, tamanio, nombre);
        }

        public HttpResponseMessage GetTodo(string filtro)
        {
            return GetResultado(0, int.MaxValue, filtro);
        }

        private HttpResponseMessage GetResultado(int pagina, int tamanio, string nombre)
        {
            List<Producto> resultado = Repositorio.ListaProductos.Where(x => x.Active && x.Nombre.ToUpper().Contains(nombre.ToUpper())).ToList();
            return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    TotalFilas = resultado.Count(),
                    NumeroPaginas = Convert.ToInt32(resultado.Count()/tamanio),
                    Resultado = resultado.Skip(pagina*tamanio).Take(tamanio)
                });
        }
        
    }
}