using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class HoldingsResponse
    {
        
        [DataMember]
        public HoldingSummaryResponse HoldingSummary { get; set; }

        [DataMember]
        public List<UserContributionData> Transactions { get; set; }

        [DataMember]
        public ExitRequestResponse ExitRequestResponse { get; set; }
    }
}
