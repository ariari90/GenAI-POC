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
        

        [DataMember]
        public string ViewExitRequestForSchemeName { get; set; }

        [DataMember]
        public DateRange ViewTransactionDateRange { get; set; }
    }
}
