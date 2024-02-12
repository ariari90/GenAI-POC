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
    public class BankBranchServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public string BankBranch { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  MobileServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                var bankInfo = service.ViewBankInfo(Request.UniqueId);

                SetDSFVariable(this, AggregatorConstants.BankBranch, bankInfo.BankBranch);
                SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
            }

            return base.Execute(executionContext);
        }
    }
}
