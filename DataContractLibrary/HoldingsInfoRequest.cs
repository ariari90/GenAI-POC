using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class HoldingsInfoRequest
    {
        private string viewExitRequestForSchemeName;
        private DateRange transactionDateRange;


        [DataMember]
        public string ViewExitRequestForSchemeName { get => viewExitRequestForSchemeName; set => viewExitRequestForSchemeName = value; }

        [DataMember]
        public DateRange ViewTransactionDateRange { get => transactionDateRange; set => transactionDateRange = value; }
    }
}
