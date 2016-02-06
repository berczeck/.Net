using Ninject.Modules;
using PatronStrategy;

namespace Ninject
{
    internal class Contenedor : NinjectModule
    {
        public override void Load()
        {
            Bind<FlujoCobro>().To<FlujoCobro>();
            Bind<ICobroRegistro>().To<CobroRegistroPersonaJuridica>().Named("PersonaJuridica");
            Bind<ICobroRegistro>().To<CobroRegistroPersonaNatural>().Named("PersonaNatural");

            Bind<IDataBase>().To<SqlDataBase>().Named("Sql");
            Bind<IDataBase>().To<OracleDataBase>().Named("Oracle");
            
            Bind<IDataTypeGenerator>().To<SqlVarcharGenerator>().WhenInjectedInto<SqlDataBase>();
            Bind<IDataTypeGenerator>().To<OracleVarcharGenerator>().WhenInjectedInto<OracleDataBase>();
        }
    }
}
