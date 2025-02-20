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
    public class BankAddressServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public string Address { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  MobileServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                BankInfo bankInfo = null;
                AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();

                try
                {
                    bankInfo = service.ViewBankInfo(Request.UniqueId);
                }
                catch (Exception e)
                {
                    DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                    throw new Exception("Workflow error: " + e.ToString());
                }
                finally
                {
                    SetDSFVariable(this, AggregatorConstants.Address, bankInfo.Address);
                    SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
                }
            }

            return base.Execute(executionContext);
        }
    }
}
