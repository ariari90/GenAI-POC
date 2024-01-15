using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class UpdateRequest
    {
        private ChangeSchemeRequest changeSchemeRequest;
        private PersonalDetails personalDetails;
        private string fundManagerName;

        [DataMember]
        public PersonalDetails PersonalDetails { get => personalDetails; set => personalDetails = value; }

        [DataMember]
        public ChangeSchemeRequest ChangeSchemeRequest { get => changeSchemeRequest; set => changeSchemeRequest = value; }

        [DataMember]
        public string FundManagerName { get => fundManagerName; set => fundManagerName = value; }
    }
}
