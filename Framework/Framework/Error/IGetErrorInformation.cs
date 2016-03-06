using System;

namespace Framework.Error
{
    public interface IHandlerErrorInformation
    {
        void AddExceptionInformation(MessageFormat messageFormat, Exception excepcion);
    }
}
