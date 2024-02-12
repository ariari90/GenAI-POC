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
    public class TransactionsServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public List<UserContributionData> Transactions { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  MobileServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;
            
            
            if (Request != null && Request.HoldingsInfoRequest.ViewTransactionDateRange != null && Request.HoldingsInfoRequest.ViewTransactionDateRange.StartDate != null && Request.HoldingsInfoRequest.ViewTransactionDateRange.EndDate != null)
            {
                Console.WriteLine("Request is null");
                AccountBankingService.AccountBankingServiceClient service = new AccountBankingService.AccountBankingServiceClient();
                var transactions = service.GetUserContribution(Request.UniqueId,
                        Request.HoldingsInfoRequest.ViewTransactionDateRange.StartDate.Value, Request.HoldingsInfoRequest.ViewTransactionDateRange.EndDate.Value);


                SetDSFVariable(this, AggregatorConstants.Transactions, transactions.ToList());
                SetDSFRequiredResponse(AggregatorConstants.HoldingsResponse);
            }

            return base.Execute(executionContext);
        }
    }
}
