﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AggregatorSvcService.ContributionService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ValidationResponse", Namespace="http://schemas.datacontract.org/2004/07/DataContractLibrary")]
    [System.SerializableAttribute()]
    public partial class ValidationResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValidationMessageField;
        
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
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ValidationMessage {
            get {
                return this.ValidationMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ValidationMessageField, value) != true)) {
                    this.ValidationMessageField = value;
                    this.RaisePropertyChanged("ValidationMessage");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ContributionService.IContributionService")]
    public interface IContributionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContributionService/ContributeOnline", ReplyAction="http://tempuri.org/IContributionService/ContributeOnlineResponse")]
        AggregatorSvcService.ContributionService.ValidationResponse ContributeOnline(int uniqueId, string product, int units);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContributionService/ChangeSchemePreference", ReplyAction="http://tempuri.org/IContributionService/ChangeSchemePreferenceResponse")]
        AggregatorSvcService.ContributionService.ValidationResponse ChangeSchemePreference(int uniqueId, int newSchemePreferenceId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContributionService/ChangeFundManagerName", ReplyAction="http://tempuri.org/IContributionService/ChangeFundManagerNameResponse")]
        AggregatorSvcService.ContributionService.ValidationResponse ChangeFundManagerName(int uniqueId, string fundManagerName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IContributionServiceChannel : AggregatorSvcService.ContributionService.IContributionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ContributionServiceClient : System.ServiceModel.ClientBase<AggregatorSvcService.ContributionService.IContributionService>, AggregatorSvcService.ContributionService.IContributionService {
        
        public ContributionServiceClient() {
        }
        
        public ContributionServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ContributionServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContributionServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContributionServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AggregatorSvcService.ContributionService.ValidationResponse ContributeOnline(int uniqueId, string product, int units) {
            return base.Channel.ContributeOnline(uniqueId, product, units);
        }
        
        public AggregatorSvcService.ContributionService.ValidationResponse ChangeSchemePreference(int uniqueId, int newSchemePreferenceId) {
            return base.Channel.ChangeSchemePreference(uniqueId, newSchemePreferenceId);
        }
        
        public AggregatorSvcService.ContributionService.ValidationResponse ChangeFundManagerName(int uniqueId, string fundManagerName) {
            return base.Channel.ChangeFundManagerName(uniqueId, fundManagerName);
        }
    }
}