using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class LoanPaymentData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int LoanId { get; set; }
        [DataMember]
        public int UniqueId { get; set; }
        [DataMember]
        public decimal PaymentAmount { get; set; }
        [DataMember]
        public DateTime PaymentDate { get; set; }
    }
}
