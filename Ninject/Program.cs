using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.ContextPreservation;
using Ninject.Extensions.NamedScope;
using PatronStrategy;

namespace Ninject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var kernelNamed = new StandardKernel(new ContenedorServicio());

            var databaseNombrada = kernelNamed.Get<IDataBase>();

            IKernel kernel = new StandardKernel(new Contenedor());

            var persona = new PersonaJuridica();
            var flujo = kernel.Get<FlujoCobro>();
            flujo.CobroRegistro = kernel.Get<ICobroRegistro>(persona.GetType().Name);

            flujo.RelizarCobro(persona);

            var database = kernel.Get<IDataBase>("Sql");
            Console.WriteLine(database.VarcharGenerator.Generate());
        }
    }
}
