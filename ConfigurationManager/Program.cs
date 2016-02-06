using System;
using System.Linq;

namespace ConfigurationManager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //ConfigManager.ConfigManager.DevMode = true;

            Configuracion exampleConfig =
                ConfigManager.ConfigManager.GetCreateConfig<Configuracion>("Configuracion");

            Console.WriteLine(exampleConfig.Mensaje);
            Console.WriteLine(exampleConfig.Nombre);
            exampleConfig.Valores.ForEach(Console.WriteLine);
            Console.WriteLine(exampleConfig.Datos.Id); 
            Console.WriteLine(exampleConfig.Datos.Codigo);

            exampleConfig.Llaves.Keys.ToList().ForEach(x => Console.WriteLine(exampleConfig.Llaves[x]));

            Console.ReadLine();
        }
    }
}
