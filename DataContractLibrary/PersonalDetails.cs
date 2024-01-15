using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class PersonalDetails
    {
        [DataMember]
        public int Uniqueid { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Mobile { get; set; }

        [DataMember]
        public int PinCode { get; set; }
    }
}
