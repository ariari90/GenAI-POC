using InfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class InfoServiceResponse
    {
        private PersonalInfo personalInfo;
        private BankInfo bankInfo;
        private List<SchemaInfo> schemas;

        [DataMember]
        public PersonalInfo PersonalInfo { get => personalInfo; set => personalInfo = value; }

        [DataMember]
        public BankInfo BankInfo { get => bankInfo; set => bankInfo = value; }

        [DataMember]
        public List<SchemaInfo> Schemas { get => schemas; set => schemas = value; }
    }
}
