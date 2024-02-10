using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class ContributionRequest
    {
        
        [DataMember]
        public string Product { get; set; }

        [DataMember]
        public int Units { get; set; }

        
    }
}
