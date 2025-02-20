using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContractLibrary
{

    [DataContract]
    public class TransactionSummaryData
    {
        [DataMember]
        public int TransactionSummaryId { get; set; }
        [DataMember]
        public int UniqueId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public DateTime TransactionDate { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public string TransactionType { get; set; }
    }
}
