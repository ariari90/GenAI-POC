using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using Common;

namespace DSP
{
    public class ContributeServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  ContributeServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null && Request.ContributionRequest != null)
            {
                ValidationResponse validationResponse = new ValidationResponse();
                try
                {
                    ContributionService.IContributionService service = new ContributionService.ContributionServiceClient();
                    validationResponse = service.ContributeOnline(Request.UniqueId, Request.ContributionRequest.Product, Request.ContributionRequest.Units);
                }
                catch (Exception e)
                {
                    DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                    throw new Exception("Workflow error: " + e.ToString());
                }
                finally
                {
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
