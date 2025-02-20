using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Entities
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
