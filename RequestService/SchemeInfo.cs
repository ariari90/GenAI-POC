//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RequestService
{
    using System;
    using System.Collections.Generic;
    
    public partial class SchemeInfo
    {
        public int schemeId { get; set; }
        public string schemeName { get; set; }
        public string fundManagerName { get; set; }
        public int percantageContribution { get; set; }
        public int uniqueId { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ExitDate { get; set; }
        public Nullable<bool> IsPreferred { get; set; }
    
        public virtual UserAccount UserAccount { get; set; }
    }
}
