using System;
using System.Transactions;
using MsMqCliente.Proxy;

namespace MsMqCliente
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Proxy.OrderProcessorClient();
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Make a queued call to submit the purchase order.
                client.SubmitPurchaseOrder(new PurchaseOrder { PONumber = Guid.NewGuid().ToString() });
                // Complete the transaction.
                scope.Complete();
            }

            //Closing the client gracefully closes the connection and cleans up resources.
            client.Close();
        }
    }
}
