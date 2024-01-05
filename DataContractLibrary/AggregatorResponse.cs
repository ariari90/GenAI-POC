using InfoService;
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
        InfoServiceResponse infoServiceResponse;

        public AggregatorResponse()
        {
            InfoServiceResponse = new InfoServiceResponse();
        }

        [DataMember]
        public InfoServiceResponse InfoServiceResponse { get => infoServiceResponse; set => infoServiceResponse = value; }
    }
}
