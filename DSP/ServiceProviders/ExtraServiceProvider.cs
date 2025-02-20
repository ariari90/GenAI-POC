using AccountInfo;
using Common.Entities;
using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;

namespace DSP.ServiceProviders
{
    public class ExtraServiceProvider : ServiceProviderBase
    {
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public List<HoldingSummaryData> HoldingSummaryData { get; set; }
        public List<HoldingSummaryData> BankingFundsDetails { get; set; }
        public List<GroupingHoldingSummaryData> HoldingsUserWise { get; set; }
        public List<GroupingHoldingSummaryData> HoldingsOfGoldSchemeUserWise { get; set; }
        public List<GroupingHoldingSummaryData> HoldingsWithInMinimumUserWise { get; set; }
        public List<GroupingHoldingSummaryData> HoldingsWithInMaximumNavUserWise { get; set; }
        public List<GroupingHoldingSummaryData> AllocatedHoldingsUserWise { get; set; }
        public List<GroupingHoldingSummaryData> RedeemedHoldingsUserWise { get; set; }
        public List<GroupingHoldingSummaryData> MinimumNoOfunitsHoldingsUserWise { get; set; }
        public List<GroupingHoldingSummaryData> MaximumNoOfUnitsHoldingsUserWise { get; set; }
        public List<GroupingHoldingSummaryData> BestSchemeUserWise { get; set; }
        public List<HoldingSummaryData> HoldingsReportByDatesRange { get; set; }
        public List<TransactionalHoldingSummaryData> TransactionalHoldingsSummaryData { get; set; }
        public List<TransactionalHoldingSummaryData> AllocatedTransactionalHoldingsSummaryData { get; set; }
        public List<TransactionalHoldingSummaryData> RedeemedTransactionalHoldingsSummaryData { get; set; }
        public List<TransactionalHoldingSummaryData> TransactionalHoldingsSummaryDataForDatesRange { get; set; }
        public decimal TotalUnits { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Nav { get; set; }
        public decimal Amount { get; set; }
        public TransactionSummaryResponse TransactionSummaryResponse { get; set; }
        public List<GroupingTransactionSummaryData> NoOfTransctionsUserWise { get; set; }
        public List<GroupingTransactionSummaryData> NoOfDepostTransctionsUserWise { get; set; }
        public List<GroupingTransactionSummaryData> NoOfWithdrawalsTransctionsUserWise { get; set; }
        public List<GroupingTransactionSummaryData> NoOfMinimumDepostTransctionsUserWise { get; set; }
        public List<GroupingTransactionSummaryData> NoOfMaximumDepostTransctionsUserWise { get; set; }
        public List<GroupingTransactionSummaryData> NoOfMinimumWithdrawalTransctionsUserWise { get; set; }
        public List<GroupingTransactionSummaryData> NoOfMaximumWithdrawalTransctionsUserWise { get; set; }
        public List<TransactionSummaryData> TransactionalReportByDatesRange { get; set; }
        public List<TransactionSummaryData> TransactionSummary { get; set; }
        public decimal DepostAmountOnly { get; set; }
        public decimal LowDepostAmountOnly { get; set; }
        public decimal HighDepostAmountOnly { get; set; }
        public decimal WithdrawalAmountOnly { get; set; }
        public decimal LowWithdrawalAmountOnly { get; set; }
        public decimal HighWithdrawalAmountOnly { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  ExtraServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            try
            {
                if (Request != null)
                {
                    AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                    bankInfo = service.ViewBankInfo(Request.UniqueId);  // placeholder
                    DSPLogger.LogMessage("Request is empty");
                    ValidationResponse validationResponse = new ValidationResponse();
                    validationResponse.ValidationMessage = "Request is empty";
                    SetValidationResponse(validationResponse);
                }

                AccountHolding accountHolding = new AccountHolding();
                int uniqueId = Request.UniqueId;
                var holdingSummaryData = accountHolding.GetHoldingSummary(uniqueId);

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
