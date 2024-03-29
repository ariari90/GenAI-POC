﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSP.AccountBankingService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AccountBankingService.IAccountBankingService")]
    public interface IAccountBankingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountBankingService/GetHoldingSummary", ReplyAction="http://tempuri.org/IAccountBankingService/GetHoldingSummaryResponse")]
        Common.Entities.HoldingSummaryResponse GetHoldingSummary(int uid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountBankingService/GetUserContribution", ReplyAction="http://tempuri.org/IAccountBankingService/GetUserContributionResponse")]
        Common.Entities.UserContributionData[] GetUserContribution(int uid, System.DateTime startdate, System.DateTime enddate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountBankingService/UpdatePersonalDetails", ReplyAction="http://tempuri.org/IAccountBankingService/UpdatePersonalDetailsResponse")]
        Common.Entities.ValidationResponse UpdatePersonalDetails(Common.Entities.PersonalDetails personDetails);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAccountBankingServiceChannel : DSP.AccountBankingService.IAccountBankingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AccountBankingServiceClient : System.ServiceModel.ClientBase<DSP.AccountBankingService.IAccountBankingService>, DSP.AccountBankingService.IAccountBankingService {
        
        public AccountBankingServiceClient() {
        }
        
        public AccountBankingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AccountBankingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccountBankingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccountBankingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Common.Entities.HoldingSummaryResponse GetHoldingSummary(int uid) {
            return base.Channel.GetHoldingSummary(uid);
        }
        
        public Common.Entities.UserContributionData[] GetUserContribution(int uid, System.DateTime startdate, System.DateTime enddate) {
            return base.Channel.GetUserContribution(uid, startdate, enddate);
        }
        
        public Common.Entities.ValidationResponse UpdatePersonalDetails(Common.Entities.PersonalDetails personDetails) {
            return base.Channel.UpdatePersonalDetails(personDetails);
        }
    }
}
