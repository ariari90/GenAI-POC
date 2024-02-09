using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class AggregatorRequest
    {
        
        [DataMember]
        public int UniqueId { get; set; }

        [DataMember]
        public ContributionRequest ContributionRequest { get; set; }

        [DataMember]
        public WithdrawRequest WithdrawRequest { get; set; }
        
        [DataMember]
        public RequestType RequestType { get; set; }

        [DataMember]
        public UpdateRequest UpdateRequest { get; set; }

        [DataMember]
        public FacilityRequest FacilityRequest { get; set; }

        [DataMember]
        public HoldingsInfoRequest HoldingsInfoRequest { get; set; }
    }
}
