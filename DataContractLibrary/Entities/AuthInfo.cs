using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class AuthInfo
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
