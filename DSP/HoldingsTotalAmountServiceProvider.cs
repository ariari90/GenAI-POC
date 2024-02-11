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
    public class HoldingsTotalAmountServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public decimal TotalAmount { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  MobileServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                Console.WriteLine("Request is null");
                AccountBankingService.AccountBankingServiceClient service = new AccountBankingService.AccountBankingServiceClient();
                var holdings = service.GetHoldingSummary(Request.UniqueId);

                SetDSFVariable(this, AggregatorConstants.TotalAmount, holdings.TotalAmount);
                SetDSFRequiredResponse(AggregatorConstants.HoldingsResponse);
            }

            return base.Execute(executionContext);
        }
    }
}
