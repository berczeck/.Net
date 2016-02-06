using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MvcApiRouting.Controllers
{
    [RoutePrefix("api/tierra/pais")]
    public class PaisController : ApiController
    {
        [Route("todo")]
        [HttpGet]
        public IEnumerable<string> GetBooks()
        {
            return new List<string> {"qwqw", "sddf", "hfghgf"};
        }

        [Route("peru/{id:int}")]
        [HttpGet]
        public IEnumerable<string> GetBooks(string id)
        {
            return new List<string> {"qwqw", "sddf", "hfghgf", id};
        }

        [HttpGet]
        public IEnumerable<string> GetBooksRuta()
        {
            return new List<string> {"rutaaa"};
        }

        [Route("~/apiWeb/tierra")]
        [HttpGet]
        public IEnumerable<string> GetBooksNew()
        {
            return new List<string> {"qwqw", "sddf", "hfghgf"};
        }

        [Route("chile")]
        [HttpGet]
        public IEnumerable<string> GetBooksUrl(string id = null)
        {
            return new List<string> {"qwqw", "sddf", "hfghgf", id};
        }

        [Route("chile")]
        [HttpGet]
        public IEnumerable<string> GetBooksUrlNombre(string id, string nombre = null)
        {
            return new List<string> {"qwqw", "sddf", "hfghgf", id, nombre};
        }

        [Route("usa")]
        [HttpPost]
        public IHttpActionResult Usa()
        {
            return Ok();
        }

        [Route("mexico")]
        [HttpPost]
        public IHttpActionResult Mexico()
        {
            return Ok();
        }
}
}
