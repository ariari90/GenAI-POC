using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContractLibrary
{

    [DataContract]
    public class GroupingTransactionSummaryData
    {
        [DataMember]
        public int GroupingId { get; set; }
        [DataMember]
        public List<TransactionSummaryData> TransactionsSummaryData { get; set; }
    }
}
