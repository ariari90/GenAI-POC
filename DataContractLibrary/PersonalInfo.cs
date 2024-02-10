using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class PersonalInfo
    {
        

        [DataMember]
        public int UniqueId { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string FathersName { get; set; }

        [DataMember]
        public string MothersName { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string Nationality { get; set; }

        [DataMember]
        public bool IsKycDone { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public int PinCode { get; set; }

        [DataMember]
        public string Mobile { get; set; }
    }
}
