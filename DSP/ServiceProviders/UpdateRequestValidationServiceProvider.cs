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
    public class UpdateRequestValidationServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  UpdateRequestValidationServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (!ValidateRequest())
            {
                ValidationResponse validationResponse = new ValidationResponse();
                validationResponse.Status = "Reject";
                validationResponse.ValidationMessage = "Inputs of update request are not provided correctly";
                SetValidationResponse(validationResponse);
                executionContext.CloseActivity();
                return ActivityExecutionStatus.Closed;
            }

            return base.Execute(executionContext);
        }

        public bool ValidateRequest()
        {
            if (Request.UpdateRequest == null)
            {
                return false;
            }

            if (Request.UpdateRequest.ChangeSchemeRequest == null && Request.UpdateRequest.PersonalDetails == null && String.IsNullOrEmpty(Request.UpdateRequest.FundManagerName))
            {
                return false;
            }

            return true;
        }
    }
}
