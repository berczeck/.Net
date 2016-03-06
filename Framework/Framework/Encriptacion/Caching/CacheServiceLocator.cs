using System;
using Ig.Framework.Comun;

namespace Ig.Framework.Caching
{
    public class CacheServiceLocator
    {
        public ICacheProvider CurrentClassCache
        {
            get
            {
                string log = HelperConfig.GetString("Ig:Framework:CacheType");

                string tipo = string.IsNullOrWhiteSpace(log)
                    ? "Ig.Framework.Caching.MemoryCacheProvider, Ig.Framework"
                    : log;

                Type tipoLog = Type.GetType(tipo);

                if (tipoLog != null)
                {
                    return (ICacheProvider)Activator.CreateInstance(tipoLog);
                }

                throw new ApplicationException(
                    "Ig.Framework: No se puede cargar el tipo especificado en 'Ig:Framework:CacheType'.");
            }
        }

    }
}
