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
    public class WithdrawServiceProvider: ServiceProviderBase
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

            if (Request.WithdrawRequest != null)
            {
                WithdrawService.WithdrawServiceClient service = new WithdrawService.WithdrawServiceClient();
                if (!Request.WithdrawRequest.IsExitRequest)
                {
                    var validationResponse = service.WithdrawT1Amount(Request.UniqueId, Request.WithdrawRequest.Product, Request.WithdrawRequest.WithdrawPercent);
                    if (validationResponse.Status != "Success")
                    {
                        SetValidationResponse(validationResponse);
                    }
                }
            }

            return base.Execute(executionContext);
        }
    }
}
