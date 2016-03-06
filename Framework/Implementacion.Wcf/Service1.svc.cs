using System;
using CuttingEdge.Conditions;
using Framework.Validacion;

namespace Implementacion.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Servicio : IServicio
    {

        //1. Referenciar el ensamblado Framework e Framework.Wcf
        //2. Referenciar el ensamblado NLog
        //3. Configurar los metodos OperationCOntract con el atributo  [FaultContract(typeof(ErrorServicioRespuesta))]
        //4. Agregar el archivo Nlog.config y actualizar el formato
        //5. Configurar el archivo web.config para el manejo de errores y trazas

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new Exception("Error de ejemplo");
        }

        public string GetDataErrorValidacion(int value)
        {
            Condicion.ValidarParametro(value).IsGreaterThan(10000);

            return value.ToString();
        }

        public string GetDataErrorRegla(int value)
        {
            Condicion.ValidarRegla(value, "R001").IsGreaterThan(100000);

            return value.ToString();
        }
    }
}
