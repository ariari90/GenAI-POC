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
    public class UpdatePersonalInfoServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  UpdatePersonalInfoServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request.UpdateRequest != null && Request.UpdateRequest.PersonalDetails != null)
            {
                PersonalDetails personalDetails = null;
                ValidationResponse validationResponse = null;
                try
                {
                    AccountBankingService.IAccountBankingService bankingService = new AccountBankingService.AccountBankingServiceClient();
                    personalDetails = Request.UpdateRequest.PersonalDetails;
                    personalDetails.Uniqueid = Request.UniqueId;
                    validationResponse = bankingService.UpdatePersonalDetails(Request.UpdateRequest.PersonalDetails);
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
