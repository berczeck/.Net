namespace Code2Read.WebCI.Controllers
{
    using System.Web.Http;

    public class CalculadoraController : ApiController
    {
        [HttpGet]
        public int Suma(int a, int b)
        {
            return a + b;
        }

        [HttpGet]
        public int Resta(int a, int b)
        {
            return a - b;
        }
    }
}
