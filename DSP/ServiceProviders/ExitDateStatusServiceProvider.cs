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
    public class ExitDateStatusServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public string Status { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  ExitDateStatusServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                if (!String.IsNullOrEmpty(Request.HoldingsInfoRequest.ViewExitRequestForSchemeName))
                {
                    WithdrawService.IWithdrawService service = null;
                    ExitRequestResponse exitRequest = null;
                    try
                    {
                        service = new WithdrawService.WithdrawServiceClient();
                        exitRequest = service.GetExitStatus(Request.UniqueId, Request.HoldingsInfoRequest.ViewExitRequestForSchemeName);
                    }
                    catch (Exception e)
                    {
                        DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                        throw new Exception("Workflow error: " + e.ToString());
                    }
                    finally
                    {
                        if (exitRequest != null)
                        {
                            SetDSFVariable(this, AggregatorConstants.ExitDateStatus, exitRequest.Status);
                            SetDSFRequiredResponse(AggregatorConstants.HoldingsResponse);
                        }
                    }
                }

            }

            return base.Execute(executionContext);
        }
    }
}
