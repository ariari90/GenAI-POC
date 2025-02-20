using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Common.Entities
{
    [DataContract]
    [XmlRoot(ElementName = "AggregatorResponse")]
    [XmlType("Common.Entities.AggregatorResponse")]
    public class AggregatorResponse
    {
        
        [DataMember]
        [XmlElement(IsNullable = true)]
        //[XmlElement(ElementName = "AccountInfoResponse")]
        //[XmlChoiceIdentifierAttribute("AccountInfoResponse")]
        public InfoServiceResponse AccountInfoResponse { get; set; }

        [DataMember]
        [XmlElement(IsNullable = true)]
        //[XmlElement(ElementName = "HoldingsResponse")]
        //[XmlChoiceIdentifierAttribute]
        public HoldingsResponse HoldingsResponse { get; set; }

        [DataMember]
        [XmlElement(IsNullable = true)]
        //[XmlElement(ElementName = "ValidationResponse")]
        //[XmlChoiceIdentifierAttribute]
        public ValidationResponse ValidationResponse { get; set; }

        [DataMember]
        [XmlElement(IsNullable = true)]
        public ExtraSvcResponse ExtraSvcResponse { get; set; }
    }
}
