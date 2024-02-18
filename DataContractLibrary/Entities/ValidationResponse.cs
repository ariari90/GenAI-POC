using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Common.Entities
{
    [XmlType("Common.Entities.ValidationResponse")]
    [DataContract]
    public class ValidationResponse
    {
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ValidationMessage { get; set; }
    }
}
