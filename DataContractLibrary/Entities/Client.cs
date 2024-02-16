using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common.Entities
{
    [DataContract]
    public class Client
    {
        private int clientId;
        private string clientName;
        

        [DataMember]
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        [DataMember]
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }
    }
}
