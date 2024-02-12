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
    public class UpdateFundManagerServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  UpdateFundManagerServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request.UpdateRequest != null && !String.IsNullOrEmpty(Request.UpdateRequest.FundManagerName))
            {
                ContributionService.ContributionServiceClient service = new ContributionService.ContributionServiceClient();
                var validationResponse = service.ChangeFundManagerName(Request.UniqueId, Request.UpdateRequest.FundManagerName);
                if (validationResponse.Status != "Success")
                {
                    SetValidationResponse(validationResponse);
                }
            }

            return base.Execute(executionContext);
        }
    }
}
