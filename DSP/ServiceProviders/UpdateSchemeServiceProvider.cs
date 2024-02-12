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
    public class UpdateSchemeServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  UpdateSchemeServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request.UpdateRequest != null && Request.UpdateRequest.ChangeSchemeRequest != null)
            {
                ContributionService.ContributionServiceClient service = new ContributionService.ContributionServiceClient();
                var validationResponse = service.ChangeSchemePreference(Request.UniqueId, Request.UpdateRequest.ChangeSchemeRequest.NewSchemeId);
                if (validationResponse.Status != "Success")
                {
                    SetValidationResponse(validationResponse);
                }
            }

            return base.Execute(executionContext);
        }
    }
}
