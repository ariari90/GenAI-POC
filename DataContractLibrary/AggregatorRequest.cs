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
        private float amount;


        [DataMember]
        public RequestType RequestType
        {
            get { return requestType; }
            set { requestType = value; }
        }

        [DataMember]
        public int UniqueId { get => uniqueId; set => uniqueId = value; }

        [DataMember]
        public float Amount { get => amount; set => amount = value; }
    }
}
