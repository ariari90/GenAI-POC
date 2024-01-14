using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace InfoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAccountInfoService
    {
        [OperationContract]
        PersonalInfo ViewPersonalInfo(int uniqueId);

        [OperationContract]
        BankInfo ViewBankInfo(int uniqueId);

        [OperationContract]
        List<SchemeInfo> GetCurrentSchemeDetails(int uniqueId);

        [OperationContract]
        SchemeInfo GetSchemePreference(int uniqueId);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class PersonalInfo
    {
        int uniqueId;
        string name;
        string fathersName;
        string mothersName;
        DateTime dateOfBirth;
        string gender;
        string nationality;
        bool isKycDone;
        string address1;
        string address2;
        string city;
        int pinCode;
        string mobile;
       

        [DataMember]
        public int UniqueId { get => uniqueId; set => uniqueId = value; }

        [DataMember]
        public string Name { get => name; set => name = value; }
        
        [DataMember]
        public string FathersName { get => fathersName; set => fathersName = value; }

        [DataMember]
        public string MothersName { get => mothersName; set => mothersName = value; }

        [DataMember]
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        [DataMember]
        public string Gender { get => gender; set => gender = value; }

        [DataMember]
        public string Nationality { get => nationality; set => nationality = value; }

        [DataMember]
        public bool IsKycDone { get => isKycDone; set => isKycDone = value; }

        [DataMember]
        public string Address1 { get => address1; set => address1 = value; }

        [DataMember]
        public string Address2 { get => address2; set => address2 = value; }

        [DataMember]
        public string City { get => city; set => city = value; }

        [DataMember]
        public int PinCode { get => pinCode; set => pinCode = value; }

        [DataMember]
        public string Mobile { get => mobile; set => mobile = value; }

        
    }

    [DataContract]
    public class BankInfo
    {
        int uniqueId;
        int accountNumber;
        string bankBranch;
        string bankName;
        string address;
        string ifscCode;

        [DataMember]
        public int UniqueId { get => uniqueId; set => uniqueId = value; }

        [DataMember]
        public int AccountNumber { get => accountNumber; set => accountNumber = value; }

        [DataMember]
        public string BankBranch { get => bankBranch; set => bankBranch = value; }

        [DataMember]
        public string BankName { get => bankName; set => bankName = value; }

        [DataMember]
        public string Address { get => address; set => address = value; }

        [DataMember]
        public string IfscCode { get => ifscCode; set => ifscCode = value; }
    }

    [DataContract]
    public class SchemeInfo
    {
        int schemaId;
        string schemeName;
        string fundManagerName;
        int percantageContribution;
        DateTime createdDate;
        DateTime exitDate;
        bool isPreferred;

        [DataMember]
        public int SchemeId { get => schemaId; set => schemaId = value; }

        [DataMember]
        public string SchemeName { get => schemeName; set => schemeName = value; }

        [DataMember]
        public string FundManagerName { get => fundManagerName; set => fundManagerName = value; }

        [DataMember]
        public int PercantageContribution { get => percantageContribution; set => percantageContribution = value; }

        [DataMember]
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }

        [DataMember]
        public DateTime ExitDate { get => exitDate; set => exitDate = value; }

        [DataMember]
        public bool IsPreferred { get => isPreferred; set => isPreferred = value; }
    }
}
