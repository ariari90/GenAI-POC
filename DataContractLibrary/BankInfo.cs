using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class BankInfo
    {
        int uniqueId;
        int accountNumber;
        string bankBranch;
        string bankName;
        string address;
        string ifscCode;

        [DataMember]
        public int UniqueId { get => uniqueId; set => uniqueId = value; }

        [DataMember]
        public int AccountNumber { get => accountNumber; set => accountNumber = value; }

        [DataMember]
        public string BankBranch { get => bankBranch; set => bankBranch = value; }

        [DataMember]
        public string BankName { get => bankName; set => bankName = value; }

        [DataMember]
        public string Address { get => address; set => address = value; }

        [DataMember]
        public string IfscCode { get => ifscCode; set => ifscCode = value; }
    }
}
