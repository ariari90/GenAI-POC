using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataContractLibrary
{
    [DataContract]
    public class SecurityRequest
    {
        private int securitytId;
        private string securityType;
        private decimal securityValue;
        private string securityStatus;
        

        [DataMember]
        public int SecuritytId
        {
            get { return securitytId; }
            set { securitytId = value; }
        }

        [DataMember]
        public String SecurityType
        {
            get { return securityType; }
            set { securityType = value; }
        }

        [DataMember]
        public Decimal SecurityValue
        {
            get { return securityValue; }
            set { securityValue = value; }
        }

        [DataMember]
        public String SecurityStatus
        {
            get { return securityStatus; }
            set { securityStatus = value; }
        }

    }
}
