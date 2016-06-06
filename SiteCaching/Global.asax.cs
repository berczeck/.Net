using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SiteCaching.Models;

namespace SiteCaching
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            List<Persona> lista = new List<Persona>();
            List<PersonaCustomizada> listaOpt = new List<PersonaCustomizada>();


            for (int i = 0; i < 1000; i++)
            {
                Persona p = new Persona
                {
                    Id = i,
                    ApellidoMaterno = "Apellido Materno " + i,
                    ApellidoPaterno = "Apellido Paterno " + i,
                    Email = "email" + i + "@gmail.com",
                    FechaNacimiento = DateTime.Now,
                    Nombre = "Nombre " + i,
                    TipoSexo = i%2,
                    Pedidos = new List<Pedido>()
                };

                lista.Add(p);

                Persona pc = new Persona
                {
                    Id = i,
                    ApellidoMaterno = "Apellido Materno " + i,
                    ApellidoPaterno = "Apellido Paterno " + i,
                    Email = "email" + i + "@gmail.com",
                    FechaNacimiento = DateTime.Now,
                    Nombre = "Nombre " + i,
                    TipoSexo = i % 2,
                    Pedidos = new List<Pedido>()
                };

                PersonaCustomizada pcOpt = new PersonaCustomizada
                {
                    Id = i,
                    ApellidoMaterno = "Apellido Materno " + i,
                    ApellidoPaterno = "Apellido Paterno " + i,
                    Email = "email" + i + "@gmail.com",
                    FechaNacimiento = DateTime.Now,
                    Nombre = "Nombre " + i,
                    TipoSexo = i % 2,
                    Pedidos = new List<Pedido>()
                };

                HttpContext.Current.Cache.Add("Persona" + i, pc, null, DateTime.Today.AddHours(6),
                    Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);

                HttpContext.Current.Cache.Add("PersonaOpt" + i, pcOpt, null, DateTime.Today.AddHours(6),
                    Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);
            }

            HttpContext.Current.Cache.Add("listaPersona", lista, null, DateTime.Today.AddHours(6),
                Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);
            HttpContext.Current.Cache.Add("listaPersonaOpt", listaOpt, null, DateTime.Today.AddHours(6),
                Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);
        }
    }
}
