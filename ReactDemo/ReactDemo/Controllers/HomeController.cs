using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.SignalR;
using ReactDemo.Models;

namespace ReactDemo.Controllers
{
    using Hubs;

    public class HomeController : Controller
    {
        //private static readonly IList<CommentModel> _comments;

        //static HomeController()
        //{
        //    _comments = new List<CommentModel>
        //    {
        //        new CommentModel
        //        {
        //            Author = "Daniel Lo Nigro",
        //            Text = "Hello ReactJS.NET World!"
        //        },
        //        new CommentModel
        //        {
        //            Author = "Pete Hunt",
        //            Text = "This is one comment"
        //        },
        //        new CommentModel
        //        {
        //            Author = "Jordan Walke",
        //            Text = "This is *another* comment"
        //        },
        //    };
        //}

        private static readonly RepositorioComentarios repositorio = new RepositorioComentarios();

        public ActionResult Index()
        {
            var _comments = repositorio.TraerTodo().Reverse();
            return View(_comments);
        }

        public ActionResult ComentarioPanel()
        {
            var _comments = repositorio.TraerTodo().Reverse();
            return View(_comments);
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult Comments()
        {
            var _comments = repositorio.TraerTodo().Reverse();
            return Json(_comments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel comment)
        {
            //repositorio.Agregar(comment);
            //NotificarComentarioNuevo();
            //return Content("Success :)");
            comment.Identificador = Guid.NewGuid().ToString();
            ServiceBusConfiguracion.EnviarMensaje(comment);

            return Content(comment.Identificador);
        }

        //private void NotificarComentarioNuevo()
        //{
        //    var _comments = repositorio.TraerTodo();
        //    var hubContext = GlobalHost.ConnectionManager.GetHubContext<CommentHub>();
        //    hubContext.Clients.All.loadComments(_comments);
        //}
    }
}