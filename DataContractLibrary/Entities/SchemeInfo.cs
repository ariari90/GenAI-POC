using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class SchemeInfo
    {
        

        [DataMember]
        public int SchemeId { get; set; }

        [DataMember]
        public string SchemeName { get; set; }

        [DataMember]
        public string FundManagerName { get; set; }

        [DataMember]
        public int PercantageContribution { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ExitDate { get; set; }

        [DataMember]
        public bool IsPreferred { get; set; }
    }
}
