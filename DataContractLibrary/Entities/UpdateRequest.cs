using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class UpdateRequest
    {
        
        [DataMember]
        public PersonalDetails PersonalDetails { get; set; }

        [DataMember]
        public ChangeSchemeRequest ChangeSchemeRequest { get; set; }

        [DataMember]
        public string FundManagerName { get; set; }
    }
}
