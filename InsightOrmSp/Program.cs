using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Database;

namespace InsightOrmSp
{
    class Program
    {
        static void Main(string[] args)
        {
            var rep = new Repositorio();
            var persona = new Persona {Email = "aaaa", Nombre = "yyy", FechaNacimiento = DateTime.Now};
            rep.Insertar(persona);
            var resultado = rep.ObtenerPorId(persona.Id);

            var resultadoManual = rep.ObtenerPorIdManual(persona.Id);
            
            Console.WriteLine(persona.Nombre);
            Console.WriteLine(resultado.Nombre);

            try
            {

                for (int i = 0; i < 100; i++)
                {
                    var resultado3 = rep.ObtenerPorNombre<Persona>(null);

                    Console.WriteLine(resultado3.Nombre);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
