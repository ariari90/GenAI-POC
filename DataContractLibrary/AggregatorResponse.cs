using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class AggregatorResponse
    {
        private InfoServiceResponse infoServiceResponse;
        private HoldingsResponse holdingsResponse;
        private ValidationResponse validationResponse;
        private FacilityResponse facilityResponse;

        [DataMember]
        public InfoServiceResponse AccountInfoResponse { get => infoServiceResponse; set => infoServiceResponse = value; }

        [DataMember]
        public HoldingsResponse HoldingsResponse { get => holdingsResponse; set => holdingsResponse = value; }

        [DataMember]
        public ValidationResponse ValidationResponse { get => validationResponse; set => validationResponse = value; }

        [DataMember]
        public FacilityResponse FacilityResponse { get => facilityResponse; set => facilityResponse = value; }
    }
}
