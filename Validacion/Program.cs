using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CuttingEdge.Conditions;

namespace Validacion
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lista = new List<string>();

            Invariants.Invariant(0,"venta", "0001").IsLessOrEqual(-2,"Erro de negocio no s epeude validar menor a 2");

            Invariants.Invariant(0, "venta").IsLessOrEqual(-2, "Erro de negocio no s epeude validar menor a 2"); 

            Condition.Requires(lista, "lista").Evaluate(x => x.Count > 0,"lista vacia");

            Condition.Requires(0, "Id").IsGreaterThan(0);

            Customer entity = new Customer { FirstName = "", LastName = "" };

           new ValidacionDataAnnotation().Validar(entity);

            try
            {
                //new ValidacionCondition().Validar(null);
                entity.FirstName = "naaa";
                new ValidacionCondition().Validar(entity);

                Condition.WithExceptionOnFailure<Exception>().Requires("dd", "nombre").IsNotNullOrEmpty();
                Condition.Requires(2, "Id").IsGreaterThan(1,"el id no peude se rmenor a 1");
                string t = "hola";
                Condition.Requires(t, "Nombre")
                    .IsNotNullOrEmpty("el nombre no peude ser nulo")
                    .StartsWith("h")
                    .Evaluate(x => x.EndsWith("a"));

                t = null;

                Condition.Requires(t, "Nombre")
                    .IsNotNullOrWhiteSpace();

                string valor = Prueba();

                Condition.Ensures(valor, "codigo").IsNotNull("EL codigo no peude ser nulo");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private static string Prueba()
        {
            return null;
        }
    }
}
