using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class ChangeSchemeRequest
    {  
        

        [DataMember]
        public int NewSchemeId { get; set; }
    }
}
