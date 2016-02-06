using System;
using System.Collections.Generic;
using System.Linq;
using EfConvenciones.Generador;

namespace EfConvenciones
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var param = new Persona
            {
                Id = 1,
                Nombre = null,
                FechaNacimiento = DateTime.Now,
                EmpresaId = 0,
                PaisId = 1
            };

            var pais = new Pais
            {
                Id = 1
            };

            IFormatoOperadorQuery queryInclusion = new FormatoOperadorQueryInclusion();
            queryInclusion.Inspector = new InspectorTipoComplejo();

            IFormatoOperadorQuery queryExclusion = new FormatoOperadorQueryExclusion();
            queryExclusion.Inspector = new InspectorTipoNativo("ApellidoMaterno");

            var resultado = queryExclusion.GenerarQuery(queryInclusion.GenerarQuery(param), "ardilla");
            Console.WriteLine(resultado.Filtro);

            queryExclusion.Inspector = new InspectorDiccionario();

            var resultado2 = queryExclusion.GenerarQuery(resultado,
                new Dictionary<string, object> {{"Iniciado", false}, {"Cancelado", true}});
            Console.WriteLine(resultado2.Filtro);

            Contexto contexto = new Contexto();

            //contexto.Database.CreateIfNotExists();

            var r = (from p in contexto.Empresa
                join e in contexto.Persona on p.Id equals e.EmpresaId into g
                from pe in g.DefaultIfEmpty()
                where p.Nombre.Contains("p") && pe.Id == 0
                select pe).ToList();

            var t = (from p in contexto.Persona
                join e in contexto.Empresa on p.EmpresaId equals e.Id
                where e.Nombre.Contains("p") && p.Id == 0
                select new {p, e}).ToList();

            var o = (from p in contexto.Pais
                join e in contexto.Persona on p.Id equals e.PaisId into g
                from pe in g.DefaultIfEmpty()
                where p.Nombre.Contains("p") && pe.Id == 0
                select pe).ToList();

            Repositorio<Persona> repPersona = new Repositorio<Persona>(contexto);
            repPersona.Listar(new Persona {Id = 3});
            repPersona.Listar(new Persona {Nombre = "NOmbre", Id = 3, FechaNacimiento = DateTime.Now});
            repPersona.Listar(new Dictionary<string, object>
            {
                {"Id", 0},
                {"PaisId", 2},
                {"EmpresaId", 2},
                {"Nombre", "Alex"}
            });

            var primero = repPersona.Get(x => x.Id == 3 || x.Empresa == null, x => x.OrderBy(tt => tt.Id),x => x.Empresa,x => x.FechaNacimiento);
            var tercero = repPersona.Listar(new { Id = 3 });

            Repositorio<Pais> repPais = new Repositorio<Pais>(contexto);
            repPais.Listar(new Pais {Id = 1});
            repPais.Listar(new Pais {Nombre = "NOmbre"});

            Console.WriteLine("Fin");
            Console.ReadLine();
        }
    }
}
