using System;
using Framework.Caching;

namespace Implementacion.Caching
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Referenciar el ensamblado Framework
            //2. Importar el namespace Framework.Caching

            CustomCacheManager.Instancia.Agregar("Key","Hola");

            var valor = CustomCacheManager.Instancia.Obtener("Key");

            Console.WriteLine(valor);

            CustomCacheManager.Instancia.Remover("Key");

            valor = CustomCacheManager.Instancia.Obtener("Key");

            Console.WriteLine(valor);

            //Llamando directo al objeto, para escenarios de DI
            ICacheProvider cache = new MemoryCacheProvider();

            cache.Agregar("Key","Hola");
            valor = cache.Obtener("Key");
            Console.WriteLine(valor);

            cache.Remover("Key");
            valor = cache.Obtener("Key");

            Console.WriteLine(valor);
            Console.Read();
        }
    }
}
