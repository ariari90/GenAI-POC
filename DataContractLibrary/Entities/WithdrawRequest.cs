using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class WithdrawRequest
    {
 
        [DataMember]
        public string Product { get; set; }

        [DataMember]
        public decimal WithdrawPercent { get; set; }

        [DataMember]
        public bool IsExitRequest { get; set; }
    }
}
