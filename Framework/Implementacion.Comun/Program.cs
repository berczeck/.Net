using System;
using Framework.Comun;

namespace Implementacion.Comun
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Referenciar el ensamblado Framework
            //2. Importar el namespace Framework.Comun

            var noExiste = HelperConfig.GetString("NoExiste");
            Console.WriteLine(noExiste);

            var existe = HelperConfig.GetString("Existe");
            Console.WriteLine(existe);

            var valorEntero = HelperConfig.GetInt32("valorEntero");
            Console.WriteLine(valorEntero);

            var valorInt16 = HelperConfig.GetInt16("valorInt16");
            Console.WriteLine(valorInt16);

            var valorBool = HelperConfig.GetBool("valorBool");
            Console.WriteLine(valorBool);

            var valorDecimal = HelperConfig.GetDecimal("valorDecimal");
            Console.WriteLine(valorDecimal);

            Console.ReadLine();
        }
    }
}
