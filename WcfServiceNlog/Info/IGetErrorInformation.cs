using System;

namespace WcfServiceNlog.Info
{
    public interface IHandlerErrorInformation<in T> where T : Exception
    {
        void AddExceptionInformation(MessageFormat messageFormat,T excepcion);
    }
}
