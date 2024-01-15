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
        string product;
        int units;
        bool changeSchemePreference;

        [DataMember]
        public string Product { get => product; set => product = value; }

        [DataMember]
        public int Units { get => units; set => units = value; }

        
    }
}
