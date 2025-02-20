using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContractLibrary
{
    [DataContract]
    public class GroupingHoldingSummaryData
    {
        [DataMember]
        public int GroupingId { get; set; }
        [DataMember]
        public List<HoldingSummaryData> HoldingSummaryData { get; set; }
    }
}
