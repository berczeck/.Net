using System;
using System.Runtime.Caching;
using CuttingEdge.Conditions;
using Framework.Validacion;

namespace Framework.Caching
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private readonly ObjectCache _objectCache;

        public MemoryCacheProvider()
        {
            _objectCache = MemoryCache.Default;
        }

        public void Agregar(string key, object valor)
        {
            Condicion.ValidarParametro(key).IsNotNullOrWhiteSpace("La llave no puede ser nula o vacia.");
            Condicion.ValidarParametro(valor).IsNotNull("El valor no puede ser nulo.");
            var politica = new CacheItemPolicy();
            _objectCache.Add(key, valor, politica);
        }

        public void Agregar(string key, object valor, TimeSpan timeout)
        {
            Condicion.ValidarParametro(key).IsNotNullOrWhiteSpace("La llave no puede ser nula o vacia.");
            Condicion.ValidarParametro(valor).IsNotNull("El valor no puede ser nulo.");
            
            _objectCache.Add(key, valor, DateTime.Now.AddMinutes(timeout.Seconds));
        }

        public object Obtener(string key)
        {
            Condicion.ValidarParametro(key).IsNotNullOrWhiteSpace("La llave no puede ser nula o vacia.");
            return _objectCache.Get(key);
        }

        public object this[string key]
        {
            get
            {
                Condicion.ValidarParametro(key).IsNotNullOrWhiteSpace("La llave no puede ser nula o vacia.");
                return _objectCache[key];
            }
            set
            {
                Condicion.ValidarParametro(key).IsNotNullOrWhiteSpace("La llave no puede ser nula o vacia.");
                _objectCache[key] = value;
            }
        }

        public bool Remover(string key)
        {
            Condicion.ValidarParametro(key).IsNotNullOrWhiteSpace("La llave no puede ser nula o vacia.");

            if (_objectCache.Contains(key))
            {
                _objectCache.Remove(key);
                return true;
            }
            return false;
        }
    }
}
