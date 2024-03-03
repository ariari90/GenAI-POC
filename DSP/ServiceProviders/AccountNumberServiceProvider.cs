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
    public class AccountNumberServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public int AccountNumber { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  AccountNumberServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;
            BankInfo bankInfo = null;

            try
            {
                if (Request != null)
                {
                    AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                    bankInfo = service.ViewBankInfo(Request.UniqueId);
                }
            }
            catch (Exception e)
            {
                DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                throw new Exception("Workflow error: " + e.ToString());
            }
            finally
            {
                if (bankInfo != null)
                {
                    SetDSFVariable(this, AggregatorConstants.AccountNumber, bankInfo.AccountNumber);
                    SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
                }
                
            }
            return base.Execute(executionContext);
        }
    }
}
