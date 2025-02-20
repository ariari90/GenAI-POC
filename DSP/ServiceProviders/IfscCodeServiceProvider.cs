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
    public class IfscCodeServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public string IfscCode { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  IfscCodeServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                BankInfo bankInfo = null;
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
                finally
                {
                    if (bankInfo != null)
                    {
                        SetDSFVariable(this, AggregatorConstants.IfscCode, bankInfo.IfscCode);
                        SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
                    }
                }
            }

            return base.Execute(executionContext);
        }
    }
}
