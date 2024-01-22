using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace DataContractLibrary
{
    public class InitializeResponse : CodeActivity
    {
        //[RequiredArgument]
        //public InArgument<ApprovalRequest> Request { get; set; }
        //[RequiredArgument]
        //public InArgument<bool> Approved { get; set; }
        [RequiredArgument]
        public OutArgument<FacilityResponse> Response { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Response.Set(context, new FacilityResponse());
        }
    }
}
