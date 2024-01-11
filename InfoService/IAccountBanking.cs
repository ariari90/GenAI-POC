using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InfoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAccountBanking
    {
        [OperationContract]
        List<HoldingSummaryData> GetHoldingSummary(int uid);

        [OperationContract]
        List<UserContributionData> GetUserContribution(int uid, DateTime startdate, DateTime enddate);

        [OperationContract]
        ValidationResponse UpdatePersonalDetails(PersonalDetails personDetails);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class HoldingSummaryData
    {
        [DataMember]
        public int Uniqueid { get; set; }

        [DataMember]
        public string SchemeName { get; set; }

        [DataMember]
        public decimal TotalUnits { get; set; }

        [DataMember]
        public decimal Nav { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ExitDate { get; set; }

    }

    [DataContract]
    public class UserContributionData
    {
        [DataMember]
        public int Uniqueid { get; set; }

        [DataMember]
        public DateTime TransactionDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string TransactionType { get; set; }
    }

    [DataContract]
    public class PersonalDetails
    {
        [DataMember]
        public int Uniqueid { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Mobile { get; set; }

        [DataMember]
        public int PinCode { get; set; }
    }

    public class ValidationResponse
    {
        public string Status { get; set; }
    }
}

