using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace LogTakeTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernelNamed = new StandardKernel(new Contenedor());

            //
            var rep = kernelNamed.Get<IRepositorioDemo>();

            //var rep = new RepositorioDemo();

            rep.Agregar();

            Console.WriteLine("Fin");
            Console.ReadLine();
        }
    }


    public class LogTime<T>
    {
        public T Recurso { get; set; }
    }

    
    public class RepositorioDemo : IRepositorioDemo
    {

        
        public void Agregar()
        {
            System.Threading.Thread.Sleep(3000);
        }

        [TransactionInterceptor]
        private void Grabar() { }
    }

    public interface IRepositorioDemo
    {
        void Agregar();
    }
}
