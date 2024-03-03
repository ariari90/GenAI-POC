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
    public class TransactionsServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public List<UserContributionData> Transactions { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  TransactionServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;
            
            
            if (Request != null && Request.HoldingsInfoRequest.ViewTransactionDateRange != null && Request.HoldingsInfoRequest.ViewTransactionDateRange.StartDate != null && Request.HoldingsInfoRequest.ViewTransactionDateRange.EndDate != null)
            {
                UserContributionData[] transactions = null;
                try
                {
                    AccountBankingService.AccountBankingServiceClient service = new AccountBankingService.AccountBankingServiceClient();
                    transactions = service.GetUserContribution(Request.UniqueId,
                            Request.HoldingsInfoRequest.ViewTransactionDateRange.StartDate.Value, Request.HoldingsInfoRequest.ViewTransactionDateRange.EndDate.Value);
                }
                catch (Exception e)
                {
                    DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                    throw new Exception("Workflow error: " + e.ToString());
                }
                finally
                {
                    if (transactions != null)
                    {
                        SetDSFVariable(this, AggregatorConstants.Transactions, transactions.ToList());
                        SetDSFRequiredResponse(AggregatorConstants.HoldingsResponse);
                    }
                }
            }

            return base.Execute(executionContext);
        }
    }
}
