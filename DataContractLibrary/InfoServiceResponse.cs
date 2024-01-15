using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DataContractLibrary;

namespace DataContractLibrary
{
    [DataContract]
    public class InfoServiceResponse
    {
        private PersonalInfo personalInfo;
        private BankInfo bankInfo;
        private List<SchemeInfo> schemas;
        private SchemeInfo preferredScheme;

        [DataMember]
        public PersonalInfo PersonalInfo { get => personalInfo; set => personalInfo = value; }

        [DataMember]
        public BankInfo BankInfo { get => bankInfo; set => bankInfo = value; }

        [DataMember]
        public List<SchemeInfo> Schemas { get => schemas; set => schemas = value; }

        [DataMember]
        public SchemeInfo PreferredScheme { get => preferredScheme; set => preferredScheme = value; }
    }
}
