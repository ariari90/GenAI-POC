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

        [DataMember]
        public InfoServiceResponse AccountInfoResponse { get => infoServiceResponse; set => infoServiceResponse = value; }

        [DataMember]
        public HoldingsResponse HoldingsResponse { get => holdingsResponse; set => holdingsResponse = value; }

        [DataMember]
        public ValidationResponse ValidationResponse { get => validationResponse; set => validationResponse = value; }

    }
}
