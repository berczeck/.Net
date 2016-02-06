﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MsMqCliente.Proxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PurchaseOrder", Namespace="http://Microsoft.ServiceModel.Samples")]
    [System.SerializableAttribute()]
    public partial class PurchaseOrder : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PONumberField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomerId {
            get {
                return this.CustomerIdField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerIdField, value) != true)) {
                    this.CustomerIdField = value;
                    this.RaisePropertyChanged("CustomerId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PONumber {
            get {
                return this.PONumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PONumberField, value) != true)) {
                    this.PONumberField = value;
                    this.RaisePropertyChanged("PONumber");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Microsoft.ServiceModel.Samples", ConfigurationName="Proxy.IOrderProcessor")]
    public interface IOrderProcessor {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://Microsoft.ServiceModel.Samples/IOrderProcessor/SubmitPurchaseOrder")]
        void SubmitPurchaseOrder(MsMqCliente.Proxy.PurchaseOrder po);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderProcessorChannel : MsMqCliente.Proxy.IOrderProcessor, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderProcessorClient : System.ServiceModel.ClientBase<MsMqCliente.Proxy.IOrderProcessor>, MsMqCliente.Proxy.IOrderProcessor {
        
        public OrderProcessorClient() {
        }
        
        public OrderProcessorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderProcessorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderProcessorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderProcessorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SubmitPurchaseOrder(MsMqCliente.Proxy.PurchaseOrder po) {
            base.Channel.SubmitPurchaseOrder(po);
        }
    }
}
