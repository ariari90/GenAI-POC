using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class ExitRequestResponse
    {
        private int uniqueId;
        private DateTime dateRaised;
        private string status;

        [DataMember]
        public int UniqueId { get => uniqueId; set => uniqueId = value; }

        [DataMember]
        public DateTime DateRaised { get => dateRaised; set => dateRaised = value; }

        [DataMember]
        public string Status { get => status; set => status = value; }
    }
}
