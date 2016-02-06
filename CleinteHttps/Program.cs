using System;
using CleinteHttps.Proxy;

namespace CleinteHttps
{
    //http://www.codeproject.com/Articles/698862/Custom-Authentication-and-Authorization-in-WCF
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Proxy.ServicioClient proxy = new ServicioClient();
                proxy.ClientCredentials.UserName.UserName = "admin";
                proxy.ClientCredentials.UserName.Password = "admin";

                var r = proxy.GetData(10);

                Console.WriteLine(r);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("fin");
            Console.ReadLine();
        }
    }
}
