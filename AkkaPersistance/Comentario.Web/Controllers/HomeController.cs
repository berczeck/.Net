using System.Web.Mvc;
using Comentario.Core;
using Comentario.Web.Models;
using Akka.Actor;
using System;

namespace Comentario.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Get()
        {
            Actores.PanelRespuestaControlador.Tell("");
            return View("Index");
        }

        public ActionResult Post()
        {
            Actores.PanelRespuestaControlador.Tell(new Core.Comentario {
                Autor = "Alex",
                FechaHora = DateTime.Now.ToString("yyyyMMddhhmmss"),
                Identificador = Guid.NewGuid().ToString(),
                Texto = "Hello World"
            });
            return View("Index");
        }

        public ActionResult Deposito(string cliente = "berceck")
        {
            Actores.CuentaAhorroRespuestaControlador.Tell(new Deposito(cliente, 10));
            return View("Index");
        }
    }
}