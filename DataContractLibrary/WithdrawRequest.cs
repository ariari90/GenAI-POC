using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class WithdrawRequest
    {
        string product;
        decimal withdrawPercent;
        bool isExitRequest;

        [DataMember]
        public string Product { get => product; set => product = value; }

        [DataMember]
        public decimal WithdrawPercent { get => withdrawPercent; set => withdrawPercent = value; }

        [DataMember]
        public bool IsExitRequest { get => isExitRequest; set => isExitRequest = value; }
    }
}
