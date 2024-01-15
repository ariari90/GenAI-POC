using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class DateRange
    {
        private DateTime? startDate;
        private DateTime? endDate;

        [DataMember]
        public DateTime? StartDate { get => startDate; set => startDate = value; }

        [DataMember]
        public DateTime? EndDate { get => endDate; set => endDate = value; }
    }
}
