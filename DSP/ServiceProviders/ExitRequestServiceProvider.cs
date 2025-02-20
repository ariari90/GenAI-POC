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
    public class ExitRequestServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  ExitRequestProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request.WithdrawRequest != null && Request.WithdrawRequest.IsExitRequest)
            {
                ValidationResponse validationResponse = null;
                try
                {
                    WithdrawService.WithdrawServiceClient service = new WithdrawService.WithdrawServiceClient();
                    validationResponse = service.ExitRequest(Request.UniqueId, Request.WithdrawRequest.Product);
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

            return base.Execute(executionContext);
        }
    }
}
