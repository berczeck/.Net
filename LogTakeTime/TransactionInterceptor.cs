using System.Transactions;
using Ninject.Extensions.Interception;

namespace LogTakeTime
{
    public class TransactionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            using (var scope = new TransactionScope())
            {
                invocation.Proceed();

                scope.Complete();
            }
        }
    }
}
