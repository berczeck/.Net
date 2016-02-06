using Ninject.Extensions.Interception.Attributes;

namespace LogTakeTime
{
    public class LoggingInterceptorAttribute : InterceptAttribute
    {
        public override Ninject.Extensions.Interception.IInterceptor CreateInterceptor(Ninject.Extensions.Interception.Request.IProxyRequest request)
        {
            return new LoggingInterceptor();
        }
    }
}
