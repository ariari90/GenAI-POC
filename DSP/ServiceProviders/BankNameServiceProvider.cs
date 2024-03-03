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
    public class BankNameServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public string BankName { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing BankNameServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;
            BankInfo bankInfo = null;

            if (Request != null)
            {
                try
                {
                    AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                    bankInfo = service.ViewBankInfo(Request.UniqueId);
                }
                catch (Exception e)
                {
                    DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                    throw new Exception("Workflow error: " + e.ToString());
                }
                SetDSFVariable(this, AggregatorConstants.BankName, bankInfo.BankName);
                SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
            }

            return base.Execute(executionContext);
        }
    }
}
