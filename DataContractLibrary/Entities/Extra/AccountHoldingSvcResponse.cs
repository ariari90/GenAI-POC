using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entities
{
    [DataContract]
    public class AccountHoldingSvcResponse
    {
        [DataMember]
        public List<HoldingSummaryData> HoldingSummaryData { get; set; }
        [DataMember]
        public List<HoldingSummaryData> BankingFundsDetails { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> HoldingsUserWise { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> HoldingsOfGoldSchemeUserWise { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> HoldingsWithInMinimumUserWise { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> HoldingsWithInMaximumNavUserWise { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> AllocatedHoldingsUserWise { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> RedeemedHoldingsUserWise { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> MinimumNoOfunitsHoldingsUserWise { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> MaximumNoOfUnitsHoldingsUserWise { get; set; }
        [DataMember]
        public List<GroupingHoldingSummaryData> BestSchemeUserWise { get; set; }
        [DataMember]
        public List<HoldingSummaryData> HoldingsReportByDatesRange { get; set; }
        [DataMember]
        public List<TransactionalHoldingSummaryData> TransactionalHoldingsSummaryData { get; set; }
        [DataMember]
        public List<TransactionalHoldingSummaryData> AllocatedTransactionalHoldingsSummaryData { get; set; }
        [DataMember]
        public List<TransactionalHoldingSummaryData> RedeemedTransactionalHoldingsSummaryData { get; set; }
        [DataMember]
        public List<TransactionalHoldingSummaryData> TransactionalHoldingsSummaryDataForDatesRange { get; set; }
        [DataMember]
        public decimal TotalUnits { get; set; }
        [DataMember]
        public decimal Nav { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public TransactionSummaryResponse TransactionSummaryResponse { get; set; }
        [DataMember]
        public List<GroupingTransactionSummaryData> NoOfTransctionsUserWise { get; set; }
        [DataMember]
        public List<GroupingTransactionSummaryData> NoOfDepostTransctionsUserWise { get; set; }
        [DataMember]
        public List<GroupingTransactionSummaryData> NoOfWithdrawalsTransctionsUserWise { get; set; }
        [DataMember]
        public List<GroupingTransactionSummaryData> NoOfMinimumDepostTransctionsUserWise { get; set; }
        [DataMember]
        public List<GroupingTransactionSummaryData> NoOfMaximumDepostTransctionsUserWise { get; set; }
        [DataMember]
        public List<GroupingTransactionSummaryData> NoOfMinimumWithdrawalTransctionsUserWise { get; set; }
        [DataMember]
        public List<GroupingTransactionSummaryData> NoOfMaximumWithdrawalTransctionsUserWise { get; set; }
        [DataMember]
        public List<TransactionSummaryData> TransactionalReportByDatesRange { get; set; }
        [DataMember]
        public List<TransactionSummaryData> TransactionSummary { get; set; }
        [DataMember]
        public decimal DepostAmountOnly { get; set; }
        [DataMember]
        public decimal LowDepostAmountOnly { get; set; }
        [DataMember]
        public decimal HighDepostAmountOnly { get; set; }
        [DataMember]
        public decimal WithdrawalAmountOnly { get; set; }
        [DataMember]
        public decimal LowWithdrawalAmountOnly { get; set; }
        [DataMember]
        public decimal HighWithdrawalAmountOnly { get; set; }
    }
}
