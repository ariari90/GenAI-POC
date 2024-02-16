using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class AggregatorResponse
    {
        
        [DataMember]
        public InfoServiceResponse AccountInfoResponse { get; set; }

        [DataMember]
        public HoldingsResponse HoldingsResponse { get; set; }

        [DataMember]
        public ValidationResponse ValidationResponse { get; set; }

        [DataMember]
        public FacilityResponse FacilityResponse { get; set; }
    }
}
