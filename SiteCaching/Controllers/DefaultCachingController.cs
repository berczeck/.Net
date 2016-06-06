using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using SiteCaching.Models;

namespace SiteCaching.Controllers
{
    public class DefaultCachingController : Controller
    {
        //
        // GET: /DefaultCaching/
        public ActionResult Index()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Random rnd = new Random();

            int nroVueltas = 1000;

            for (int i = 0; i < nroVueltas; i++)
            {
                List<Persona> lista = HttpContext.Cache["listaPersona"] as List<Persona>;
                if (lista != null)
                {
                    Persona persona = lista[rnd.Next(0, nroVueltas)];
                }
            }

            TimeSpan ts = stopWatch.Elapsed;
            ViewBag.Duracion = ts.TotalMilliseconds;
            /**********************************************/
            stopWatch = new Stopwatch();
            stopWatch.Start();
            rnd = new Random();
            
            for (int i = 0; i < nroVueltas; i++)
            {
                List<PersonaCustomizada> lista = HttpContext.Cache["listaPersonaOpt"] as List<PersonaCustomizada>;
                if (lista != null)
                {
                    PersonaCustomizada persona = lista[rnd.Next(0, nroVueltas)];
                }
            }

            ts = stopWatch.Elapsed;
            ViewBag.DuracionOpt = ts.TotalMilliseconds;
            /**********************************************/
            stopWatch = new Stopwatch();
            stopWatch.Start();
            rnd = new Random();

            for (int i = 0; i < nroVueltas; i++)
            {
                Persona persona = HttpContext.Cache["Persona" + rnd.Next(0, nroVueltas)] as Persona;
            }

            ts = stopWatch.Elapsed;
            ViewBag.DuracionCustomizada = ts.TotalMilliseconds;
            /**********************************************/
            stopWatch = new Stopwatch();
            stopWatch.Start();
            rnd = new Random();

            for (int i = 0; i < nroVueltas; i++)
            {
                PersonaCustomizada persona = HttpContext.Cache["PersonaOpt" + rnd.Next(0, nroVueltas)] as PersonaCustomizada;
            }

            ts = stopWatch.Elapsed;
            ViewBag.DuracionCustomizadaOpt = ts.TotalMilliseconds;

            return View();
        }
    }
}