using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWcfAsyncVsSync.Proxy;

namespace ClientWcfAsyncVsSync
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int nroVueltas = 10;

            for (int i = 0; i < nroVueltas; i++)
            {
                try
                {
                    using (Service1Client proxy = new Service1Client())
                    {
                        proxy.GetDataUsingDataContract(new CompositeType());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Sync Tiempo:{0}", ts.TotalMilliseconds);

            for (int i = 0; i < nroVueltas; i++)
            {
                try
                {
                    using (Service1Client proxy = new Service1Client())
                    {
                        proxy.GetDataUsingDataContractAsync(new CompositeType());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            TimeSpan tsAsync = stopWatch.Elapsed;

            Console.WriteLine("Async Tiempo:{0}", tsAsync.TotalMilliseconds);

            Console.ReadLine();
        }
    }
}
