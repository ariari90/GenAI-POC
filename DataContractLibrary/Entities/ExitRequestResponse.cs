using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class ExitRequestResponse
    {
        
        [DataMember]
        public int UniqueId { get; set; }

        [DataMember]
        public Nullable<DateTime> DateRaised { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
