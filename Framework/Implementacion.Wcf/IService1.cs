using System.Runtime.Serialization;
using System.ServiceModel;
using Framework.Wcf.Error;

namespace Implementacion.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServicio
    {

        [OperationContract]
        [FaultContract(typeof(ErrorServicioRespuesta))]
        string GetData(int value);

        [OperationContract]
        [FaultContract(typeof(ErrorServicioRespuesta))]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [FaultContract(typeof(ErrorServicioRespuesta))]
        string GetDataErrorValidacion(int value);

        [OperationContract]
        [FaultContract(typeof(ErrorServicioRespuesta))]
        string GetDataErrorRegla(int value);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
