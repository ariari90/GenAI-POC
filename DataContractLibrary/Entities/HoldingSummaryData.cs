using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContractLibrary
{
    [DataContract]
    public class HoldingSummaryData
    {
        [DataMember]
        public int HoldingSummaryId { get; set; }
        [DataMember]
        public int UniqueId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string HoldingSchemeName { get; set; }
        [DataMember]
        public string OperationType { get; set; }
        [DataMember]
        public decimal TotalUnits { get; set; }
        [DataMember]
        public decimal Nav { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public DateTime TransactionDate { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public DateTime ExitDate { get; set; }
    }
}