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
        private int uniqueId;
        private RequestType requestType;
        private UpdateRequest updateRequest;
        private ContributionRequest contributionRequest;
        private WithdrawRequest withdrawRequest;
        private HoldingsInfoRequest holdingsInfoRequest;
        private FacilityRequest facilityRequest;

        [DataMember]
        public int UniqueId { get => uniqueId; set => uniqueId = value; }

        [DataMember]
        public ContributionRequest ContributionRequest { get => contributionRequest; set => contributionRequest = value; }

        [DataMember]
        public WithdrawRequest WithdrawRequest { get => withdrawRequest; set => withdrawRequest = value; }
        
        [DataMember]
        public RequestType RequestType { get => requestType; set => requestType = value; }

        [DataMember]
        public UpdateRequest UpdateRequest { get => updateRequest; set => updateRequest = value; }

        [DataMember]
        public FacilityRequest FacilityRequest { get => facilityRequest; set => facilityRequest = value; }

        [DataMember]
        public HoldingsInfoRequest HoldingsInfoRequest { get => holdingsInfoRequest; set => holdingsInfoRequest = value; }
    }
}
