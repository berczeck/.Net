using System;

namespace DapperOrmSp
{
    internal class Program
    {
        private static void Main(string[] args)
        {


            var rep = new RepositorioDapper<Persona>();

            var persona = new Persona {Email = "aaaa", Nombre = "yyy", FechaNacimiento = DateTime.Now};

            persona.Id = rep.Insertar<int>(new
            {
                persona.Nombre,
                persona.Email,
                persona.Direccion,
                persona.FechaNacimiento
            });

            var resultado = rep.ObtenerPorId(persona.Id);
            
            Console.WriteLine(persona.Nombre);
            Console.WriteLine(resultado.Nombre);

            try
            {

                for (int i = 0; i < 100; i++)
                {
                    var resultado3 = rep.ObtenerPorNombre(null);

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
