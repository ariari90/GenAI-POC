using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContractLibrary
{
    [DataContract]
    public class TransactionalHoldingSummaryData
    {
        [DataMember]
        public TransactionSummaryData TransactionSummaryData { get; set; }
        [DataMember]
        public HoldingSummaryData HoldingSummaryData { get; set; }

        public static implicit operator List<object>(TransactionalHoldingSummaryData v)
        {
            throw new NotImplementedException();
        }
    }
}
