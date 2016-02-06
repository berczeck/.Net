using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

namespace LogTakeTime
{
    internal class Contenedor : NinjectModule
    {
        public override void Load()
        {
            //Bind<RepositorioDemo>().To<RepositorioDemo>().Intercept().With<LoggingInterceptor>();
            
            //Bind<RepositorioDemo>().ToSelf();

            Bind<IRepositorioDemo>().To<RepositorioDemo>();
        }

    }
}
