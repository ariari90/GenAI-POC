﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AggregatorSvcAuth.AggregatorSvc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AggregatorSvc.IAggregatorSvc")]
    public interface IAggregatorSvc {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAggregatorSvc/GetData", ReplyAction="http://tempuri.org/IAggregatorSvc/GetDataResponse")]
        Common.Entities.AggregatorResponse GetData(Common.Entities.AggregatorRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAggregatorSvcChannel : AggregatorSvcAuth.AggregatorSvc.IAggregatorSvc, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AggregatorSvcClient : System.ServiceModel.ClientBase<AggregatorSvcAuth.AggregatorSvc.IAggregatorSvc>, AggregatorSvcAuth.AggregatorSvc.IAggregatorSvc {
        
        public AggregatorSvcClient() {
        }
        
        public AggregatorSvcClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AggregatorSvcClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AggregatorSvcClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AggregatorSvcClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Common.Entities.AggregatorResponse GetData(Common.Entities.AggregatorRequest request) {
            return base.Channel.GetData(request);
        }
    }
}
