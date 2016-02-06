using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Database;

namespace InsightOrmSp
{
    public class Repositorio
    {
        public void Insertar(Persona persona)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["insight"].ConnectionString);
            conn.Execute("PersonaInsertar", persona);
        }

        public Persona ObtenerPorId(int id)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["insight"].ConnectionString);
            //conn.Execute("PersonaObtenerPorId", id);
            return conn.Query<Persona>("PersonaObtenerPorId", new { Id = id}).FirstOrDefault();
        }

        public Persona ObtenerPorIdManual(int id)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["insight"].ConnectionString);
            return conn.QuerySql<Persona>("PersonaObtenerPorId @Id", new { Id = id }).FirstOrDefault();
        }

        public T ObtenerPorNombre<T>(string nombre) where T : class
        {
            Type tipo = typeof(T);
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["insight"].ConnectionString);
            return conn.Query<T>(tipo.Name+"ObtenerPorNombre", new { Nombre = nombre }).FirstOrDefault();
        }

    }
}
