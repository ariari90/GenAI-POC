﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AggregatorSvcService.AccountInfoService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PersonalInfo", Namespace="http://schemas.datacontract.org/2004/07/DataContractLibrary")]
    [System.SerializableAttribute()]
    public partial class PersonalInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Address1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Address2Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateOfBirthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FathersNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FullNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsKycDoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MobileField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MothersNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NationalityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PinCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UniqueIdField;
        
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
        public string Address1 {
            get {
                return this.Address1Field;
            }
            set {
                if ((object.ReferenceEquals(this.Address1Field, value) != true)) {
                    this.Address1Field = value;
                    this.RaisePropertyChanged("Address1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address2 {
            get {
                return this.Address2Field;
            }
            set {
                if ((object.ReferenceEquals(this.Address2Field, value) != true)) {
                    this.Address2Field = value;
                    this.RaisePropertyChanged("Address2");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City {
            get {
                return this.CityField;
            }
            set {
                if ((object.ReferenceEquals(this.CityField, value) != true)) {
                    this.CityField = value;
                    this.RaisePropertyChanged("City");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateOfBirth {
            get {
                return this.DateOfBirthField;
            }
            set {
                if ((this.DateOfBirthField.Equals(value) != true)) {
                    this.DateOfBirthField = value;
                    this.RaisePropertyChanged("DateOfBirth");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FathersName {
            get {
                return this.FathersNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FathersNameField, value) != true)) {
                    this.FathersNameField = value;
                    this.RaisePropertyChanged("FathersName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FullName {
            get {
                return this.FullNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FullNameField, value) != true)) {
                    this.FullNameField = value;
                    this.RaisePropertyChanged("FullName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Gender {
            get {
                return this.GenderField;
            }
            set {
                if ((object.ReferenceEquals(this.GenderField, value) != true)) {
                    this.GenderField = value;
                    this.RaisePropertyChanged("Gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsKycDone {
            get {
                return this.IsKycDoneField;
            }
            set {
                if ((this.IsKycDoneField.Equals(value) != true)) {
                    this.IsKycDoneField = value;
                    this.RaisePropertyChanged("IsKycDone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Mobile {
            get {
                return this.MobileField;
            }
            set {
                if ((object.ReferenceEquals(this.MobileField, value) != true)) {
                    this.MobileField = value;
                    this.RaisePropertyChanged("Mobile");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MothersName {
            get {
                return this.MothersNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MothersNameField, value) != true)) {
                    this.MothersNameField = value;
                    this.RaisePropertyChanged("MothersName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nationality {
            get {
                return this.NationalityField;
            }
            set {
                if ((object.ReferenceEquals(this.NationalityField, value) != true)) {
                    this.NationalityField = value;
                    this.RaisePropertyChanged("Nationality");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PinCode {
            get {
                return this.PinCodeField;
            }
            set {
                if ((this.PinCodeField.Equals(value) != true)) {
                    this.PinCodeField = value;
                    this.RaisePropertyChanged("PinCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UniqueId {
            get {
                return this.UniqueIdField;
            }
            set {
                if ((this.UniqueIdField.Equals(value) != true)) {
                    this.UniqueIdField = value;
                    this.RaisePropertyChanged("UniqueId");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BankInfo", Namespace="http://schemas.datacontract.org/2004/07/DataContractLibrary")]
    [System.SerializableAttribute()]
    public partial class BankInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AccountNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BankBranchField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BankNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IfscCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UniqueIdField;
        
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
        public int AccountNumber {
            get {
                return this.AccountNumberField;
            }
            set {
                if ((this.AccountNumberField.Equals(value) != true)) {
                    this.AccountNumberField = value;
                    this.RaisePropertyChanged("AccountNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BankBranch {
            get {
                return this.BankBranchField;
            }
            set {
                if ((object.ReferenceEquals(this.BankBranchField, value) != true)) {
                    this.BankBranchField = value;
                    this.RaisePropertyChanged("BankBranch");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BankName {
            get {
                return this.BankNameField;
            }
            set {
                if ((object.ReferenceEquals(this.BankNameField, value) != true)) {
                    this.BankNameField = value;
                    this.RaisePropertyChanged("BankName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IfscCode {
            get {
                return this.IfscCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.IfscCodeField, value) != true)) {
                    this.IfscCodeField = value;
                    this.RaisePropertyChanged("IfscCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UniqueId {
            get {
                return this.UniqueIdField;
            }
            set {
                if ((this.UniqueIdField.Equals(value) != true)) {
                    this.UniqueIdField = value;
                    this.RaisePropertyChanged("UniqueId");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SchemeInfo", Namespace="http://schemas.datacontract.org/2004/07/DataContractLibrary")]
    [System.SerializableAttribute()]
    public partial class SchemeInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreatedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ExitDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FundManagerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsPreferredField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PercantageContributionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SchemeIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SchemeNameField;
        
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
        public System.DateTime CreatedDate {
            get {
                return this.CreatedDateField;
            }
            set {
                if ((this.CreatedDateField.Equals(value) != true)) {
                    this.CreatedDateField = value;
                    this.RaisePropertyChanged("CreatedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ExitDate {
            get {
                return this.ExitDateField;
            }
            set {
                if ((this.ExitDateField.Equals(value) != true)) {
                    this.ExitDateField = value;
                    this.RaisePropertyChanged("ExitDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FundManagerName {
            get {
                return this.FundManagerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FundManagerNameField, value) != true)) {
                    this.FundManagerNameField = value;
                    this.RaisePropertyChanged("FundManagerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsPreferred {
            get {
                return this.IsPreferredField;
            }
            set {
                if ((this.IsPreferredField.Equals(value) != true)) {
                    this.IsPreferredField = value;
                    this.RaisePropertyChanged("IsPreferred");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PercantageContribution {
            get {
                return this.PercantageContributionField;
            }
            set {
                if ((this.PercantageContributionField.Equals(value) != true)) {
                    this.PercantageContributionField = value;
                    this.RaisePropertyChanged("PercantageContribution");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SchemeId {
            get {
                return this.SchemeIdField;
            }
            set {
                if ((this.SchemeIdField.Equals(value) != true)) {
                    this.SchemeIdField = value;
                    this.RaisePropertyChanged("SchemeId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SchemeName {
            get {
                return this.SchemeNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SchemeNameField, value) != true)) {
                    this.SchemeNameField = value;
                    this.RaisePropertyChanged("SchemeName");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AccountInfoService.IAccountInfoService")]
    public interface IAccountInfoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountInfoService/ViewPersonalInfo", ReplyAction="http://tempuri.org/IAccountInfoService/ViewPersonalInfoResponse")]
        AggregatorSvcService.AccountInfoService.PersonalInfo ViewPersonalInfo(int uniqueId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountInfoService/ViewBankInfo", ReplyAction="http://tempuri.org/IAccountInfoService/ViewBankInfoResponse")]
        AggregatorSvcService.AccountInfoService.BankInfo ViewBankInfo(int uniqueId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountInfoService/GetCurrentSchemeDetails", ReplyAction="http://tempuri.org/IAccountInfoService/GetCurrentSchemeDetailsResponse")]
        AggregatorSvcService.AccountInfoService.SchemeInfo[] GetCurrentSchemeDetails(int uniqueId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountInfoService/GetSchemePreference", ReplyAction="http://tempuri.org/IAccountInfoService/GetSchemePreferenceResponse")]
        AggregatorSvcService.AccountInfoService.SchemeInfo GetSchemePreference(int uniqueId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAccountInfoServiceChannel : AggregatorSvcService.AccountInfoService.IAccountInfoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AccountInfoServiceClient : System.ServiceModel.ClientBase<AggregatorSvcService.AccountInfoService.IAccountInfoService>, AggregatorSvcService.AccountInfoService.IAccountInfoService {
        
        public AccountInfoServiceClient() {
        }
        
        public AccountInfoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AccountInfoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccountInfoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccountInfoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AggregatorSvcService.AccountInfoService.PersonalInfo ViewPersonalInfo(int uniqueId) {
            return base.Channel.ViewPersonalInfo(uniqueId);
        }
        
        public AggregatorSvcService.AccountInfoService.BankInfo ViewBankInfo(int uniqueId) {
            return base.Channel.ViewBankInfo(uniqueId);
        }
        
        public AggregatorSvcService.AccountInfoService.SchemeInfo[] GetCurrentSchemeDetails(int uniqueId) {
            return base.Channel.GetCurrentSchemeDetails(uniqueId);
        }
        
        public AggregatorSvcService.AccountInfoService.SchemeInfo GetSchemePreference(int uniqueId) {
            return base.Channel.GetSchemePreference(uniqueId);
        }
    }
}