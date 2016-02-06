using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SampleMSMQ
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IOrderProcessor
    {
        [OperationContract(IsOneWay = true)]
        void SubmitPurchaseOrder(PurchaseOrder po);
    }


    [DataContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public class PurchaseOrder
    {
        [DataMember]
        public string PONumber;

        [DataMember]
        public string CustomerId;
        
    }
}
