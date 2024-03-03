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
    public class BankBranchServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public string BankBranch { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  BankBranchServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                BankInfo bankInfo = null;

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
                    if (bankInfo != null)
                    {
                        SetDSFVariable(this, AggregatorConstants.BankBranch, bankInfo.BankBranch);
                        SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
                    }
                }
            }

            return base.Execute(executionContext);
        }
    }
}
