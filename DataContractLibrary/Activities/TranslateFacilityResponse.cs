using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace DataContractLibrary.Activities
{
    public class TranslateFacilityResponse : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Dictionary<string, object>> output { get; set; }
        //[RequiredArgument]
        //public InArgument<bool> Approved { get; set; }
        [RequiredArgument]
        public OutArgument<FacilityResponse> Response { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
             Dictionary<string, object> results  = context.GetValue(output);
             Response.Set(context, results["workflowOutput"]);
        }
    }
}
