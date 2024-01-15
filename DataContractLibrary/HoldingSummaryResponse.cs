using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class HoldingSummaryResponse
    {
        [DataMember]
        public List<HoldingSummaryData> HoldingSummaryData { get; set; }

        [DataMember]
        public decimal TotalAmount { get; set; }

    }
}
