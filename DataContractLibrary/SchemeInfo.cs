using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class SchemeInfo
    {
        int schemaId;
        string schemeName;
        string fundManagerName;
        int percantageContribution;
        DateTime createdDate;
        DateTime exitDate;
        bool isPreferred;

        [DataMember]
        public int SchemeId { get => schemaId; set => schemaId = value; }

        [DataMember]
        public string SchemeName { get => schemeName; set => schemeName = value; }

        [DataMember]
        public string FundManagerName { get => fundManagerName; set => fundManagerName = value; }

        [DataMember]
        public int PercantageContribution { get => percantageContribution; set => percantageContribution = value; }

        [DataMember]
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }

        [DataMember]
        public DateTime ExitDate { get => exitDate; set => exitDate = value; }

        [DataMember]
        public bool IsPreferred { get => isPreferred; set => isPreferred = value; }
    }
}
