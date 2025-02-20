using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContractLibrary
{

    [DataContract]
    public class TransactionSummaryResponse
    {
        [DataMember]
        public List<TransactionSummaryData> TransactionSummary { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }
}
