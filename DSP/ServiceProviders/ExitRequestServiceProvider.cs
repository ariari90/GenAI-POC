using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;

namespace DSP
{
    public class ExitRequestServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  WithdrawServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request.WithdrawRequest != null && Request.WithdrawRequest.IsExitRequest)
            {
                WithdrawService.WithdrawServiceClient service = new WithdrawService.WithdrawServiceClient();
                var validationResponse = service.ExitRequest(Request.UniqueId, Request.WithdrawRequest.Product);
                if (validationResponse.Status != "Success")
                {
                    SetValidationResponse(validationResponse);
                }
            }

            return base.Execute(executionContext);
        }
    }
}
