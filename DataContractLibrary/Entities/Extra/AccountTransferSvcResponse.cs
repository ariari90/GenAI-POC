using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class AccountTransferSvcResponse
    {
        [DataMember]
        public List<TransactionSummaryData> TransactionSummaryData { get; set; }
        [DataMember]
        public List<LoanPaymentData> LoanPaymentData { get; set; }
    }
}
