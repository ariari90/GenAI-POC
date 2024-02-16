using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class HoldingSummaryData
    {
        [DataMember]
        public int Uniqueid { get; set; }

        [DataMember]
        public string HoldingSchemeName { get; set; }

        [DataMember]
        public decimal TotalUnits { get; set; }

        [DataMember]
        public decimal Nav { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ExitDate { get; set; }

    }
}
