namespace Ig.Framework.Caching
{
    public class CustomCacheManager
    {
        private static ICacheProvider _cache;
        private static readonly object SyncRoot = new object();
        private static CustomCacheManager _instancia;

        private CustomCacheManager()
        {
            if (_cache == null)
            {
                _cache = CacheBuilder.CurrentClassCache;
            }
        }

        public static CustomCacheManager Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new CustomCacheManager();
                        }
                    }
                }

                return _instancia;
            }
        }

        public CacheServiceLocator CacheBuilder
        {
            get
            {
                return new CacheServiceLocator();
            }
        }

        public void Agregar(string key, object valor)
        {
            _cache.Agregar(key, valor);
        }

        public void Agregar(string key, object valor, System.TimeSpan timeout)
        {
            _cache.Agregar(key, valor, timeout);
        }

        public object Obtener(string key)
        {
            return _cache.Obtener(key);
        }

        public bool Remover(string key)
        {
            return _cache.Remover(key);
        }
    }
}
