using System.Transactions;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;

namespace LogTakeTime
{
    public class TransactionInterceptorAttribute : InterceptAttribute
    {
        private readonly IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;
        private readonly TransactionScopeOption _transactionScopeOption = TransactionScopeOption.Required;

        public TransactionInterceptorAttribute()
        {
        }

        public TransactionInterceptorAttribute(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

        public TransactionInterceptorAttribute(TransactionScopeOption transactionScopeOption)
        {
            _transactionScopeOption = transactionScopeOption;
        }

        public TransactionInterceptorAttribute(IsolationLevel isolationLevel,TransactionScopeOption transactionScopeOption)
        {
            _transactionScopeOption = transactionScopeOption;
            _isolationLevel = isolationLevel;
        }

        public IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
        }

        public TransactionScopeOption TransactionScopeOption
        {
            get { return _transactionScopeOption; }
        }

        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return new TransactionInterceptor();
        }
    }
}
