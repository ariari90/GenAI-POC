using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class BankInfo
    {
        
        [DataMember]
        public int UniqueId { get; set; }

        [DataMember]
        public int AccountNumber { get; set; }

        [DataMember]
        public string BankBranch { get; set; }

        [DataMember]
        public string BankName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string IfscCode { get; set; }
    }
}
