using System.Configuration;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;

namespace Ninject
{
    public class ContenedorServicio: NinjectModule
    {
        private readonly string _entorno;
        public ContenedorServicio()
        {
            string entorno = ConfigurationManager.AppSettings["Entorno"];
            _entorno = string.IsNullOrWhiteSpace(entorno) ? "Sql" : entorno;
        }

        const string ScopeNameSql = "Sql";
        const string ScopeNameOracle = "Oracle";

        public override void Load()
        {
            //Bind<IDataBase>().To<SqlDataBase>().When(x => _entorno.Equals(ScopeNameSql));

            //Bind<IDataBase>().To<OracleDataBase>().When(x => _entorno.Equals(ScopeNameOracle));
            //Bind<IDataTypeGenerator>().To<OracleVarcharGenerator>().WhenInjectedInto<OracleDataBase>();

            if (_entorno.Equals(ScopeNameSql))
            {
                Bind<IDataBase>().To<SqlDataBase>();
                Bind<IDataTypeGenerator>().To<SqlVarcharGenerator>();
            }
            else
            {
                Bind<IDataBase>().To<OracleDataBase>();
                Bind<IDataTypeGenerator>().To<OracleVarcharGenerator>();
            }
        }

    }
}
