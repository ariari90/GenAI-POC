using Common.Entities;
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
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  WithdrawServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request.WithdrawRequest != null)
            {
                WithdrawService.WithdrawServiceClient service = new WithdrawService.WithdrawServiceClient();
                ValidationResponse validationResponse = new ValidationResponse();
                if (!Request.WithdrawRequest.IsExitRequest)
                {
                    try
                    {
                        validationResponse = service.WithdrawT1Amount(Request.UniqueId, Request.WithdrawRequest.Product, Request.WithdrawRequest.WithdrawPercent);
                    }
                    catch (Exception e)
                    {
                        DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                        throw new Exception("Workflow error: " + e.ToString());
                    }
                    finally
                    {
                        if (validationResponse != null && validationResponse.Status != "Success")
                        {
                            SetValidationResponse(validationResponse);
                        }
                    }
                }
            }

            return base.Execute(executionContext);
        }
    }
}
