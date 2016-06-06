using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNLog.ServiceReference1;

namespace TestNLog
{
    [TestClass]
    public class Concurrencia
    {
        [TestMethod]
        public void TestMethod1()
        {
            const int max = 1000;
            using (var servicio = new Service1Client())
            {
                servicio.SetLog("--- for ---");
                for (var i = 0; i < max; i++)
                {
                    servicio.SetLog(i.ToString());
                }

                servicio.SetLog("--- parallel.for ---");
                Parallel.For(0, max, i =>
                {
                    servicio.SetLog(i.ToString());
                });

                servicio.SetLog("--- for ---");
                for (var i = 0; i < max; i++)
                {
                    servicio.SetLog(i.ToString());
                }
            }
        }
    }
}
