using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ClientMSMQ.Referencia;

namespace ClientMSMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Referencia.OrderProcessorClient();
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Make a queued call to submit the purchase order.
                client.SubmitPurchaseOrder(new PurchaseOrder{PONumber = Guid.NewGuid().ToString()});
                // Complete the transaction.
                scope.Complete();
            }

            //Closing the client gracefully closes the connection and cleans up resources.
            client.Close();
        }
    }
}
