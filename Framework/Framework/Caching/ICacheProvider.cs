using System;

namespace Framework.Caching
{
    public interface ICacheProvider
    {
        void Agregar(string key, object valor);
        void Agregar(string key, object valor, TimeSpan timeout);
        object Obtener(string key);
        object this[string key] { get; set; }
        bool Remover(string key);
    }
}
