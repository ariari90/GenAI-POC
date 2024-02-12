using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace DataContractLibrary
{
    [DataContract]
    public class SecuredService : System.Web.Services.Protocols.SoapHeader
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string AuthenticationToken { get; set; }
    }
}