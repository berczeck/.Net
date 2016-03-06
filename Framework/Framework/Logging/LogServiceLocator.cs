using System;
using Framework.Comun;

namespace Framework.Logging
{
    public class LogServiceLocator
    {
        public ILogger CurrentClassLogger
        {
            get
            {
                string log = HelperConfig.GetString("Ig:Framework:LogType");

                string tipo = string.IsNullOrWhiteSpace(log)
                    ? "Framework.Logging.LogNet, Framework"
                    : log;

                Type tipoLog = Type.GetType(tipo);

                if (tipoLog != null)
                {
                    return (ILogger)Activator.CreateInstance(tipoLog);
                }

                throw new ApplicationException(
                    "Framework: No se puede cargar el tipo especificado en 'Ig:Framework:LogType'.");
            }
        }
    
    }
}
