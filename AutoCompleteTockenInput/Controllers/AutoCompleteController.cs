using System.Collections;
using System.Linq;
using System.Web.Http;

namespace AutoCompleteTockenInput.Controllers
{
    public class AutoCompleteController : ApiController
    {
        public IEnumerable GetPorNombre(string nombre)
        {
            return RepositorioProducto.ListaProductos.Where(x => x.Nombre.ToUpper().Contains(nombre.ToUpper()))
                .Select(x => new {id = x.Id, nombre = x.Nombre});
        }
    }
}