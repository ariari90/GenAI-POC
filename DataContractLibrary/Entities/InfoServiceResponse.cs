using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Common.Entities;
using System.Workflow.ComponentModel;

namespace Common.Entities
{
    [DataContract]
    public class InfoServiceResponse
    {
        
        [DataMember]
        public PersonalInfo PersonalInfo { get ; set; }

        [DataMember]
        public BankInfo BankInfo { get; set; }

        [DataMember]
        public List<SchemeInfo> Schemes { get; set; }

        [DataMember]
        public SchemeInfo PreferredScheme { get; set; }
    }
}
