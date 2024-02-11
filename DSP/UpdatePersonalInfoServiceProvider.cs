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
    public class UpdatePersonalInfoServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  MobileServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request.UpdateRequest != null && Request.UpdateRequest.PersonalDetails != null)
            {
                AccountBankingService.IAccountBankingService bankingService = new AccountBankingService.AccountBankingServiceClient();
                var personalDetails = Request.UpdateRequest.PersonalDetails;
                personalDetails.Uniqueid = Request.UniqueId;
                var validationResponse = bankingService.UpdatePersonalDetails(Request.UpdateRequest.PersonalDetails);
                if (validationResponse.Status != "Success")
                {
                    SetValidationResponse(validationResponse);
                }
            }

            return base.Execute(executionContext);
        }
    }
}
