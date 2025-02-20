using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class ExtraSvcResponse
    {
        [DataMember]
        public AccountHoldingSvcResponse AccountHoldingSvcResponse { get; set; }

        [DataMember]
        public AccountTransferSvcResponse AccountTransferSvcResponse { get; set; }
    }
}
