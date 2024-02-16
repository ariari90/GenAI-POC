using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class UserContributionData
    {
        [DataMember]
        public int Uniqueid { get; set; }

        [DataMember]
        public DateTime TransactionDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string TransactionType { get; set; }
    }
}
