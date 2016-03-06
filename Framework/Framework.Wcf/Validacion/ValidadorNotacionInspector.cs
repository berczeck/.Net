using System.ServiceModel.Dispatcher;
using Framework.Validacion;

namespace Framework.Wcf.Validacion
{
    public class ValidadorNotacionInspector : IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            foreach (var input in inputs)
            {
                Condicion.ValidarNotacion(input);
            }

            return null;
        }
    }
}
