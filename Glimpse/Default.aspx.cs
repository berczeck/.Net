using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using EfConvenciones;
using Glimpse.Ninject;
using Ninject;
using PatronStrategy;
using WcfServiceNlog;

namespace Glimpse
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var t = Session["nombre"];
            if (t == null)
            {
                Session.Add("nombre", "Alexxxxxxxx");
            }
            
            var lista = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

            HttpContext.Current.Cache.Add("listaCodigos", lista, null, DateTime.Today.AddHours(6),
                Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);

            lista = Cache["listaCodigos"] as int[];

            CustomLogManager.Instancia.RegistrarMensaje("Inicio:" + DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
            Thread.Sleep(3000);
            CustomLogManager.Instancia.RegistrarMensaje("Fin:" + DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));

            IKernel kernel = new StandardKernel(new Contenedor(), new GetNinjectInstanceForGlimpseModule());
            var flujo = kernel.Get<ICobroRegistro>();

            Repositorio<Persona> repPersona = new Repositorio<Persona>(new Contexto());

            try
            {
                repPersona.Listar(new Persona { Id = 1 });
            }
            catch (Exception)
            {
            }
            //repPersona.Listar(new Persona { Nombre = "NOmbre", Id = 1, FechaNacimiento = DateTime.Now });
            //repPersona.Listar(new Dictionary<string, object>
            //{
            //    {"Id", 0},
            //    {"PaisId", 2},
            //    {"EmpresaId", 2},
            //    {"Nombre", "Alex"}
            //});
        }
    }
}