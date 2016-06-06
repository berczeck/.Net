using System;

namespace WcfServiceNlog.Info
{
    public class HandlerUnknowErrorInformation : IHandlerErrorInformation<Exception>
    {
        #region Miembros de IManejoError<Exception>

        public void AddExceptionInformation(MessageFormat messageFormat, Exception excepcion)
        {
            int contExcepcionInterna = 0;

            messageFormat.AddExtraInformation("Exception.Type", excepcion.GetType().Name);
            messageFormat.AddExtraInformation("Exception.Message", excepcion.Message);

            Exception excepcionInterna = excepcion.InnerException;
            while (excepcionInterna != null)
            {
                contExcepcionInterna++;

                string tituloExcepcionInterna = string.Format("Exception.InnerException{0}", contExcepcionInterna);

                messageFormat.AddExtraInformation(tituloExcepcionInterna + ".Type", excepcion.InnerException.GetType().Name);
                messageFormat.AddExtraInformation(tituloExcepcionInterna + ".Message", excepcion.InnerException.Message);

                excepcionInterna = excepcionInterna.InnerException;
            }

            if (excepcion.StackTrace != null)
            {
                messageFormat.AddExtraInformation("Exception.StackTrace", excepcion.StackTrace);
            }
        }

        #endregion
    }
}
