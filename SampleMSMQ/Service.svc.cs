using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SampleMSMQ
{
    //http://n3wjack.net/2012/05/20/was-crashes-iis-7-while-searching-for-unexisting-services-for-msmq-endpoints/
    //http://msdn.microsoft.com/es-es/library/ms789032(v=vs.110).aspx
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class OrderProcessorService : IOrderProcessor
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitPurchaseOrder(PurchaseOrder po)
        {
            Console.WriteLine("Processing {0} ", po);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\inetpub\wwwroot\Colas\pedidos.txt", true))
            {
                file.WriteLine(po.PONumber);
            }
        }
    }
}
