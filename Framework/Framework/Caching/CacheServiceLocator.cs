using System;
using Framework.Comun;

namespace Framework.Caching
{
    public class CacheServiceLocator
    {
        public ICacheProvider CurrentClassCache
        {
            get
            {
                string log = HelperConfig.GetString("Ig:Framework:CacheType");

                string tipo = string.IsNullOrWhiteSpace(log)
                    ? "Framework.Caching.MemoryCacheProvider, Framework"
                    : log;

                Type tipoLog = Type.GetType(tipo);

                if (tipoLog != null)
                {
                    return (ICacheProvider)Activator.CreateInstance(tipoLog);
                }

                throw new ApplicationException(
                    "Framework: No se puede cargar el tipo especificado en 'Ig:Framework:CacheType'.");
            }
        }

    }
}
