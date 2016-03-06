using System;
using Framework.Encriptacion;

namespace Implementacion.Encriptacion
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Referenciar el ensamblado Framework
            //2. Importar el namespace Framework.Encriptacion

            var claveAleatoria = EncriptionService.GenerarClaveAleatoria(32);
            Console.WriteLine(claveAleatoria);

            var valorHash = EncriptionService.GenerarHash("Hola", claveAleatoria);
            Console.WriteLine(valorHash);

            var claveTdes = EncriptionService.GenerarClaveAleatoria(16);
            EncriptionProvider proveedorTdes = new TripleDesEncriptionProvider(claveTdes);

            var valorEncriptadoTdes = proveedorTdes.EncriptarTexto("Hola");
            Console.WriteLine(valorEncriptadoTdes);

            Console.WriteLine(proveedorTdes.DesencriptarTexto(valorEncriptadoTdes));

            var claveAes = EncriptionService.GenerarClaveAleatoria(32);
            EncriptionProvider proveedorAes = new AesEncriptionProvider(claveAes);

            var valorEncriptadoAes = proveedorAes.EncriptarTexto("Hola");
            Console.WriteLine(valorEncriptadoAes);

            Console.WriteLine(proveedorAes.DesencriptarTexto(valorEncriptadoAes));

            Console.ReadLine();
        }
    }
}
