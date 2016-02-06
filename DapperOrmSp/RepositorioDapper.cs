using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DapperOrmSp
{
    public class RepositorioDapper<T> where T : class
    {
        private static IDbConnection OpenConnection()
        {
            IDbConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["dapper"].ConnectionString);
            return connection;
        }

        public TR Insertar<TR>(object objeto)
        {
            using (IDbConnection conn = OpenConnection())
            {
                //conn.Execute("PersonaInsertar", persona,commandType:CommandType.StoredProcedure);
                //conn.Insert<Persona>(persona);
                //conn.Execute("PersonaInsertar @Id OUTPUT,@Nombre,@Email,@Direccion,@FechaNacimiento", persona);

                Type tipo = typeof (T);
                
                return EjecutarQuery<TR>(conn, tipo.Name + "Insertar", objeto);

                //var args = new DynamicParameters();
                //args.Add("Id", 0, direction: ParameterDirection.Output);
                //args.Add("Nombre", persona.Nombre);
                //args.Add("Email", persona.Email);
                //args.Add("Direccion", persona.Direccion);
                //args.Add("FechaNacimiento", persona.FechaNacimiento);
                //args.Add("result", 0, direction: ParameterDirection.ReturnValue);

                //conn.Execute("PersonaInsertar", args, commandType: CommandType.StoredProcedure);

                //persona.Id = args.Get<int>("Id");
            }
        }

        public T ObtenerPorId<TR>(TR id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                Type tipo = typeof(T);
                return EjecutarQuery<T>(conn, tipo.Name + "ObtenerPorId", new { Id = id });
            }
        }

        public T ObtenerPorNombre(string nombre)
        {
            Type tipo = typeof (T);
            using (IDbConnection conn = OpenConnection())
            {
                return EjecutarQuery<T>(conn, tipo.Name + "ObtenerPorNombre", new {Nombre = nombre});
            }
        }

        private TR EjecutarQuery<TR>(IDbConnection conn, string procedimiento, object param)
        {
            return
                conn.Query<TR>(procedimiento, param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
    }
}
