using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Common.Entities.Activities
{
    public class CreateFacilityResponse : CodeActivity
    {
        [RequiredArgument]
        public InArgument<FacilityRequest> Request { get; set; }

        [RequiredArgument]
        public InArgument<FacilityResponse> IntermediateOutput { get; set; }

        //[RequiredArgument]
        //public InArgument<bool> Approved { get; set; }

        [RequiredArgument]
        public OutArgument<FacilityResponse> Response { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            FacilityRequest input = context.GetValue(Request);
            FacilityResponse finalResponse = context.GetValue(IntermediateOutput);
            finalResponse.CurrentClient = input.CurrentClient;
            finalResponse.LoanAmountApproved = input.FacilityAmount;
            Response.Set(context, finalResponse);
        }
    }
}
