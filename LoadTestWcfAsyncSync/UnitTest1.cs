using System;
using LoadTestWcfAsyncSync.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoadTestWcfAsyncSync
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSync()
        {
            using (Service1Client proxy = new Service1Client())
            {
                proxy.GetDataUsingDataContract(new CompositeType());
            }
        }

        [TestMethod]
        public void TestAsync()
        {
            using (Service1Client proxy = new Service1Client())
            {
                proxy.GetDataUsingDataContractAsynCompositeType(new CompositeType());
            }
        }
    }
}
