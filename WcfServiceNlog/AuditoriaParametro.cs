using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace WcfServiceNlog
{
    
    public class AuditoriaParametro:IParameterInspector 
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            string user = "admin";
            CustomLogManager.Instancia.RegistrarTraza(operationName);
            return null;
        }
    }
}