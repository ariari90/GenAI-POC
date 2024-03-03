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
    public class TransactionRequestValidationServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  TransactionRequestValidationServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (!ValidateRequest())
            {
                ValidationResponse validationResponse = new ValidationResponse();
                validationResponse.Status = "Reject";
                validationResponse.ValidationMessage = "Contribution or Withdrawal inputs are not valid.";
                SetValidationResponse(validationResponse);
                executionContext.CloseActivity();
                return ActivityExecutionStatus.Closed;
            }

            return base.Execute(executionContext);
        }

        private bool ValidateRequest()
        {
            if (Request.ContributionRequest == null && Request.WithdrawRequest == null)
            {
                return false;
            }

            if (Request.ContributionRequest != null && String.IsNullOrEmpty(Request.ContributionRequest.Product))
            {
                return false;
            }
            return true;
        }
    }
}
