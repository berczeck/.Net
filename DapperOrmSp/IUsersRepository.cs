using System.Configuration;
using System.Data.SqlClient;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;

namespace DapperOrmSp
{
    public interface IPersonaRepository : IDataRepository<Persona>
    {
        //IUsersRepository is inheriting all CRUD operations 
    }

    public class PersonaRepository : DataRepository<Persona>, IPersonaRepository
    {
        //NOTE: Because this is a "Dependency Injection Oriented Package"
        //we need to pass the database connection and the SQL Generator as parameters
        public PersonaRepository(ISqlGenerator<Persona> sqlGenerator)
            : base(new SqlConnection(ConfigurationManager.ConnectionStrings["dapper"].ConnectionString), sqlGenerator)
        {
        }
    }
}
