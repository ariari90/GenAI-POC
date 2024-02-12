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
    public class ContributeServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  ContributeServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request.ContributionRequest != null)
            {
                ContributionService.IContributionService service = new ContributionService.ContributionServiceClient();
                var validationResponse = service.ContributeOnline(Request.UniqueId, Request.ContributionRequest.Product, Request.ContributionRequest.Units);
                if (validationResponse.Status != "Success")
                {
                    SetValidationResponse(validationResponse);
                }
            }

            return base.Execute(executionContext);
        }
    }
}
