using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Runtime.Serialization;

namespace DataContractLibrary
{
    [DataContract]
    public class FacilityRequest
    {
        private int faciliytId;
        private string faciliytType;
        
        private decimal facilityAmount;
        private string facilityStatus;
        private Client client;

        [DataMember]
        public int FaciliytId
        {
            get { return faciliytId; }
            set { faciliytId = value; }
        }

        [DataMember]
        public String FaciliytType
        {
            get { return faciliytType; }
            set { faciliytType = value; }
        }

        [DataMember]
        public Decimal FacilityAmount
        {
            get { return facilityAmount; }
            set { facilityAmount = value; }
        }


        [DataMember]
        public Client CurrentClient
        {
            get { return client; }
            set { client = value; }
        }

       
        // Need default constructor to be serializable
        public FacilityRequest()
        {

        }
        
    }
}
