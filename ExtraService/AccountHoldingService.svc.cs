using AccountInfo;
using Common.Entities;
using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ExtraService
{
    public class AccountHoldingService : IAccountHoldingService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

        private readonly string _holdingSelectQuery = string.Empty;
        private readonly string _holdingInsertQuery = string.Empty;
        private readonly string _holdingUpdateQuery = string.Empty;

        private readonly decimal _minimumBalance = 0;
        private readonly decimal _navAmountPerUnit = 0;
        private readonly decimal _minimumUnits = 0;
        private readonly decimal _maximumUnits = 0;

        AccountTransaction _accountTransaction = null;


        public AccountHoldingService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

            _holdingSelectQuery = ConfigurationManager.AppSettings["HoldingSelectQuery"];
            _holdingInsertQuery = ConfigurationManager.AppSettings["HoldingInsertQuery"];
            _holdingUpdateQuery = ConfigurationManager.AppSettings["HoldingUpdateQuery"];

            _minimumBalance = Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumBalance"]);
            _navAmountPerUnit = Convert.ToInt32(ConfigurationManager.AppSettings["NavAmountPerUnit"]);
            _minimumUnits = Convert.ToInt32(ConfigurationManager.AppSettings["MinimumUnits"]);
            _maximumUnits = Convert.ToInt32(ConfigurationManager.AppSettings["MaximumUnits"]);

            _accountTransaction = new AccountTransaction();
        }

        public AccountHoldingSvcResponse Execute(int uniqueId)
        {
            return GetHoldingSummaryWithDetails(uniqueId);
        }

        public AccountHoldingSvcResponse GetHoldingSummaryWithDetails(int uid)
        {
            DateTime startDate = new DateTime(2024, 12, 1);
            DateTime endDate = new DateTime(2024, 12, 31);

            decimal navUnitsAllocation = 750, navUnitsRedemption = 250;

            AccountHoldingSvcResponse response = new AccountHoldingSvcResponse();
            try
            {

                List<HoldingSummaryData> holdings = GetHoldingSummary();
                List<TransactionSummaryData> transactions = _accountTransaction.GetTransactionSummary();

                AllocateFunds(transactions, uid, navUnitsAllocation);
                RedeemFunds(transactions, uid, navUnitsRedemption);

                response.HoldingSummaryData = holdings;
                response.BankingFundsDetails = GetBankingFundsDetails(holdings);
                response.HoldingsUserWise = GetHoldingsUserWise(holdings);
                response.HoldingsOfGoldSchemeUserWise = GetHoldingsOfGoldSchemeUserWise(holdings);
                response.HoldingsWithInMinimumUserWise = GetHoldingsWithInMinimumUserWise(holdings);
                response.HoldingsWithInMaximumNavUserWise = GetHoldingsWithInMaximumNavUserWise(holdings);
                response.AllocatedHoldingsUserWise = GetAllocatedHoldingsUserWise(holdings);
                response.RedeemedHoldingsUserWise = GetRedeemedHoldingsUserWise(holdings);
                response.MinimumNoOfunitsHoldingsUserWise = GetMinimumNoOfunitsHoldingsUserWise(holdings);
                response.MaximumNoOfUnitsHoldingsUserWise = GetMaximumNoOfUnitsHoldingsUserWise(holdings);
                response.BestSchemeUserWise = GetBestSchemeUserWise(holdings);

                response.HoldingsReportByDatesRange = GetHoldingsReportByDatesRange(holdings, startDate, endDate);

                response.TransactionSummary = transactions;

                response.TransactionalHoldingsSummaryData = GetTransactionalHoldingsSummaryData(transactions, holdings);
                response.AllocatedTransactionalHoldingsSummaryData = GetAllocatedTransactionalHoldingsSummaryData(transactions, holdings);
                response.RedeemedTransactionalHoldingsSummaryData = GetRedeemedTransactionalHoldingsSummaryData(transactions, holdings);
                response.TransactionalHoldingsSummaryDataForDatesRange = GetTransactionalHoldingsSummaryDataForDatesRange(transactions, holdings, startDate, endDate);

                response.TotalUnits = holdings.Sum(t => t.TotalUnits);
                response.Nav = holdings.Sum(t => t.Nav);
                response.Amount = holdings.Sum(t => t.Amount);

                response.TransactionSummaryResponse = _accountTransaction.GetBalanceWithTransactionSummary(uid);

                response.NoOfTransctionsUserWise = _accountTransaction.GetNoOfTransctionsUserWise(transactions);
                response.NoOfDepostTransctionsUserWise = _accountTransaction.GetNoOfDepostTransctionsUserWise(transactions);
                response.NoOfWithdrawalsTransctionsUserWise = _accountTransaction.GetNoOfWithdrawalsTransctionsUserWise(transactions);
                response.NoOfMinimumDepostTransctionsUserWise = _accountTransaction.GetNoOfMinimumDepostTransctionsUserWise(transactions);
                response.NoOfMaximumDepostTransctionsUserWise = _accountTransaction.GetNoOfMaximumDepostTransctionsUserWise(transactions);
                response.NoOfMinimumWithdrawalTransctionsUserWise = _accountTransaction.GetNoOfMinimumWithdrawalTransctionsUserWise(transactions);
                response.NoOfMaximumWithdrawalTransctionsUserWise = _accountTransaction.GetNoOfMaximumWithdrawalTransctionsUserWise(transactions);

                response.TransactionalReportByDatesRange = _accountTransaction.GetTransactionalReportByDatesRange(transactions, startDate, endDate);
                response.TransactionSummary = _accountTransaction.GetTransactionSummary();

                response.DepostAmountOnly = _accountTransaction.GetDepostAmountOnly(transactions);
                response.LowDepostAmountOnly = _accountTransaction.GetLowDepostAmountOnly(transactions);
                response.HighDepostAmountOnly = _accountTransaction.GetHighDepostAmountOnly(transactions);
                response.WithdrawalAmountOnly = _accountTransaction.GetWithdrawalAmountOnly(transactions);
                response.LowWithdrawalAmountOnly = _accountTransaction.GetLowWithdrawalAmountOnly(transactions);
                response.HighWithdrawalAmountOnly = _accountTransaction.GetHighWithdrawalAmountOnly(transactions);

            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
            return response;

        }

        public List<HoldingSummaryData> GetHoldingSummaryData(int uid)
        {
            List<HoldingSummaryData> holdings = null;
            try
            {
                holdings = GetHoldingSummary();
                holdings = holdings.Where(h => h.UserId == uid).ToList();
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
            return holdings;

        }

        public List<HoldingSummaryData> GetHoldingSummary()
        {
            List<HoldingSummaryData> holdings = new List<HoldingSummaryData>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(_holdingSelectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Use the GetList<T> method to generate List of objects
                            holdings = reader.GetList<HoldingSummaryData>();
                        }
                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
            return holdings;
        }

        public List<HoldingSummaryData> GetBankingFundsDetails(List<HoldingSummaryData> holdings)
        {
            holdings = holdings.Where(h => h.HoldingSchemeName == "Banking and Financial").ToList();
            return holdings;
        }

        public List<GroupingHoldingSummaryData> GetHoldingsUserWise(List<HoldingSummaryData> holdings)
        {
            // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .GroupBy(t => t.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<GroupingHoldingSummaryData> GetHoldingsOfGoldSchemeUserWise(List<HoldingSummaryData> holdings)
        {
            // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .Where(h => h.HoldingSchemeName == "Gold Scheme")
                .GroupBy(h => h.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<GroupingHoldingSummaryData> GetHoldingsWithInMinimumUserWise(List<HoldingSummaryData> holdings)
        {

            decimal minimumNav = 180; //Generally it should be retrieved from markets or configuration
                                      // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .Where(h => h.Nav <= minimumNav)
                .GroupBy(h => h.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<GroupingHoldingSummaryData> GetHoldingsWithInMaximumNavUserWise(List<HoldingSummaryData> holdings)
        {
            decimal minimumNav = 50;
            decimal maximumNav = 350; //Generally it should be retrieved from markets or configuration
                                      // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .Where(h => h.Nav >= minimumNav & h.Nav <= maximumNav)
                .GroupBy(h => h.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<GroupingHoldingSummaryData> GetAllocatedHoldingsUserWise(List<HoldingSummaryData> holdings)
        {
            // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .Where(h => h.OperationType == "Hold")
                .GroupBy(h => h.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<GroupingHoldingSummaryData> GetRedeemedHoldingsUserWise(List<HoldingSummaryData> holdings)
        {
            // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .Where(h => h.OperationType == "Release")
                .GroupBy(h => h.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<GroupingHoldingSummaryData> GetMinimumNoOfunitsHoldingsUserWise(List<HoldingSummaryData> holdings)
        {
            decimal minimumNoOfUnits = 10;

            // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .Where(h => h.TotalUnits >= minimumNoOfUnits)
                .GroupBy(h => h.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<GroupingHoldingSummaryData> GetMaximumNoOfUnitsHoldingsUserWise(List<HoldingSummaryData> holdings)
        {
            decimal minimumNoOfUnits = 10;
            decimal maximumNoOfUnits = 30;

            // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .Where(h => h.TotalUnits >= minimumNoOfUnits & h.TotalUnits <= maximumNoOfUnits)
                .GroupBy(h => h.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<GroupingHoldingSummaryData> GetBestSchemeUserWise(List<HoldingSummaryData> holdings)
        {
            decimal minimumNav = 5; //Generally it should be retrieved through markets or configuration
            decimal maximumNav = 250; //Generally it should be retrieved through markets or configuration

            decimal minimumUnits = 10; //Generally it should be retrieved through markets or configuration
            decimal maximumUnits = 1000; //Generally it should be retrieved through markets or configuration

            decimal minimumAmount = 300; //Generally it should be retrieved through markets or configuration
            decimal maximumAmount = 9500; //Generally it should be retrieved through markets or configuration

            // Group holdings by UserId and project into a list of grouped results
            var groupingTransactions = holdings
                .Where(h => h.Nav >= minimumNav & h.Nav <= maximumNav &
                    h.TotalUnits >= minimumUnits & h.TotalUnits <= maximumUnits &
                    h.Amount >= minimumAmount & h.Amount <= maximumAmount)
                .GroupBy(h => h.UserId)
                .Select(group => new GroupingHoldingSummaryData
                {
                    GroupingId = group.Key,
                    HoldingSummaryData = group.ToList(),
                })
                .ToList();
            return groupingTransactions;
        }

        public List<HoldingSummaryData> GetHoldingsReportByDatesRange(List<HoldingSummaryData> holdings, DateTime startDate, DateTime endDate)
        {
            // Group holdings for given dates range
            holdings = holdings.Where(t => t.TransactionDate >= startDate & t.TransactionDate <= endDate).ToList();
            return holdings;
        }

        public List<TransactionalHoldingSummaryData> GetTransactionalHoldingsSummaryData(List<TransactionSummaryData> transactions, List<HoldingSummaryData> holdings)
        {
            // Join Transactions with holdings user based
            var transactionalHoldings = (from transaction in transactions
                                         join holding in holdings
                                         on transaction.UserId equals holding.UserId
                                         select new TransactionalHoldingSummaryData
                                         {
                                             TransactionSummaryData = new TransactionSummaryData
                                             {
                                                 UniqueId = transaction.UniqueId,
                                                 UserId = transaction.UserId,
                                                 TransactionDate = transaction.TransactionDate,
                                                 Description = transaction.Description,
                                                 Amount = transaction.Amount,
                                                 TransactionType = transaction.TransactionType
                                             },
                                             HoldingSummaryData = new HoldingSummaryData
                                             {
                                                 UniqueId = holding.UniqueId,
                                                 UserId = holding.UserId,
                                                 HoldingSchemeName = holding.HoldingSchemeName,
                                                 OperationType = holding.OperationType,
                                                 TotalUnits = holding.TotalUnits,
                                                 Nav = holding.Nav,
                                                 Amount = holding.Amount,
                                                 TransactionDate = holding.TransactionDate,
                                                 CreatedDate = holding.CreatedDate,
                                                 ExitDate = holding.ExitDate
                                             }

                                         }).ToList<TransactionalHoldingSummaryData>();
            return transactionalHoldings;
        }

        public List<TransactionalHoldingSummaryData> GetAllocatedTransactionalHoldingsSummaryData(List<TransactionSummaryData> transactions, List<HoldingSummaryData> holdings)
        {
            // Join Transactions with holdings user based
            var transactionalHoldings = (from transaction in transactions
                                         join holding in holdings
                                         on transaction.UserId equals holding.UserId
                                         where holding.OperationType == "Hold"
                                         select new TransactionalHoldingSummaryData
                                         {
                                             TransactionSummaryData = new TransactionSummaryData
                                             {
                                                 UniqueId = transaction.UniqueId,
                                                 UserId = transaction.UserId,
                                                 TransactionDate = transaction.TransactionDate,
                                                 Description = transaction.Description,
                                                 Amount = transaction.Amount,
                                                 TransactionType = transaction.TransactionType
                                             },
                                             HoldingSummaryData = new HoldingSummaryData
                                             {
                                                 UniqueId = holding.UniqueId,
                                                 UserId = holding.UserId,
                                                 HoldingSchemeName = holding.HoldingSchemeName,
                                                 OperationType = holding.OperationType,
                                                 TotalUnits = holding.TotalUnits,
                                                 Nav = holding.Nav,
                                                 Amount = holding.Amount,
                                                 TransactionDate = holding.TransactionDate,
                                                 CreatedDate = holding.CreatedDate,
                                                 ExitDate = holding.ExitDate
                                             }

                                         }).ToList<TransactionalHoldingSummaryData>();
            return transactionalHoldings;
        }

        public List<TransactionalHoldingSummaryData> GetRedeemedTransactionalHoldingsSummaryData(List<TransactionSummaryData> transactions, List<HoldingSummaryData> holdings)
        {
            // Join Transactions with holdings user based
            var transactionalHoldings = (from transaction in transactions
                                         join holding in holdings
                                         on transaction.UserId equals holding.UserId
                                         where holding.OperationType == "Release"
                                         select new TransactionalHoldingSummaryData
                                         {
                                             TransactionSummaryData = new TransactionSummaryData
                                             {
                                                 UniqueId = transaction.UniqueId,
                                                 UserId = transaction.UserId,
                                                 TransactionDate = transaction.TransactionDate,
                                                 Description = transaction.Description,
                                                 Amount = transaction.Amount,
                                                 TransactionType = transaction.TransactionType
                                             },
                                             HoldingSummaryData = new HoldingSummaryData
                                             {
                                                 UniqueId = holding.UniqueId,
                                                 UserId = holding.UserId,
                                                 HoldingSchemeName = holding.HoldingSchemeName,
                                                 OperationType = holding.OperationType,
                                                 TotalUnits = holding.TotalUnits,
                                                 Nav = holding.Nav,
                                                 Amount = holding.Amount,
                                                 TransactionDate = holding.TransactionDate,
                                                 CreatedDate = holding.CreatedDate,
                                                 ExitDate = holding.ExitDate
                                             }

                                         }).ToList<TransactionalHoldingSummaryData>();
            return transactionalHoldings;
        }

        public List<TransactionalHoldingSummaryData> GetTransactionalHoldingsSummaryDataForDatesRange(List<TransactionSummaryData> transactions, List<HoldingSummaryData> holdings, DateTime startDate, DateTime endDate)
        {
            // Join Transactions with holdings user based
            var transactionalHoldings = (from transaction in transactions
                                         join holding in holdings
                                         on transaction.UserId equals holding.UserId
                                         where transaction.TransactionDate >= startDate & transaction.TransactionDate <= endDate &
                                            holding.TransactionDate >= startDate & holding.TransactionDate <= endDate
                                         select new TransactionalHoldingSummaryData
                                         {
                                             TransactionSummaryData = new TransactionSummaryData
                                             {
                                                 UniqueId = transaction.UniqueId,
                                                 UserId = transaction.UserId,
                                                 TransactionDate = transaction.TransactionDate,
                                                 Description = transaction.Description,
                                                 Amount = transaction.Amount,
                                                 TransactionType = transaction.TransactionType
                                             },
                                             HoldingSummaryData = new HoldingSummaryData
                                             {
                                                 UniqueId = holding.UniqueId,
                                                 UserId = holding.UserId,
                                                 HoldingSchemeName = holding.HoldingSchemeName,
                                                 OperationType = holding.OperationType,
                                                 TotalUnits = holding.TotalUnits,
                                                 Nav = holding.Nav,
                                                 Amount = holding.Amount,
                                                 TransactionDate = holding.TransactionDate,
                                                 CreatedDate = holding.CreatedDate,
                                                 ExitDate = holding.ExitDate
                                             }

                                         }).ToList<TransactionalHoldingSummaryData>();
            return transactionalHoldings;
        }

        public bool AddHoldings()
        {
            bool isaffectedHoldings = false;

            // Transactions to insert
            var holdings = new List<HoldingSummaryData>
            {
                new HoldingSummaryData { UniqueId =1, UserId = 10001, HoldingSchemeName = "Banking and Financial",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId =1, UserId = 10001, HoldingSchemeName = "Banking and Financial",
                    OperationType = "Release", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 2, UserId = 10001, HoldingSchemeName = "Gold Scheme",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 2, UserId = 10001, HoldingSchemeName = "Gold Scheme",
                    OperationType = "Release", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId =1, UserId = 10002, HoldingSchemeName = "Tech Infra",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 3, UserId = 10003, HoldingSchemeName = "Tax Gain Scheme",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 4, UserId = 10004, HoldingSchemeName = "Gold Scheme",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 2, UserId = 10005, HoldingSchemeName = "Infra Fund",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 3, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 3, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Hold", TotalUnits = 40, Nav = 150, Amount = 40 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 3, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Hold", TotalUnits = 40, Nav = 150, Amount = 40 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 4, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Hold", TotalUnits = 40, Nav = 150, Amount = 40 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 5, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Hold", TotalUnits = 40, Nav = 150, Amount = 40 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 5, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Release", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 5, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Release", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 5, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Release", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 3, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Release", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 2, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 1, UserId = 10006, HoldingSchemeName = "Pharma Fund",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 1, UserId = 10006, HoldingSchemeName = "FundScheme",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
                new HoldingSummaryData { UniqueId = 2, UserId = 10006, HoldingSchemeName = "FundScheme",
                    OperationType = "Hold", TotalUnits = 25, Nav = 150, Amount = 25 * 150, TransactionDate=new DateTime(2024, 12, 20),
                    CreatedDate = new DateTime(2024, 12, 20), ExitDate = new DateTime(2026, 12, 20)},
            };

            // Insert transactions into the database
            foreach (var holding in holdings)
            {
                isaffectedHoldings = AddHolding(holding);
            }
            return isaffectedHoldings;
        }

        public bool AddHolding(HoldingSummaryData holdingSummaryData)
        {
            int affectedHoldings = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_holdingInsertQuery, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", holdingSummaryData.UniqueId);
                        command.Parameters.AddWithValue("@UserId", holdingSummaryData.UserId);
                        command.Parameters.AddWithValue("@SchemeName", holdingSummaryData.HoldingSchemeName);
                        command.Parameters.AddWithValue("@OperationType", holdingSummaryData.OperationType);
                        command.Parameters.AddWithValue("@TotalUnits", holdingSummaryData.TotalUnits);
                        command.Parameters.AddWithValue("@Nav", holdingSummaryData.Nav);
                        command.Parameters.AddWithValue("@Amount", holdingSummaryData.Amount);
                        command.Parameters.AddWithValue("@TransactionDate", holdingSummaryData.TransactionDate);
                        command.Parameters.AddWithValue("@CreatedDate", holdingSummaryData.CreatedDate);
                        command.Parameters.AddWithValue("@ExitDate", holdingSummaryData.ExitDate);

                        affectedHoldings = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
            return affectedHoldings > 0;

        }

        public bool UpdateHolding(HoldingSummaryData holdingSummaryData)
        {
            int affectedTrans = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_holdingUpdateQuery, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", holdingSummaryData.UniqueId);
                        command.Parameters.AddWithValue("@UserId", holdingSummaryData.UserId);
                        command.Parameters.AddWithValue("@SchemeName", holdingSummaryData.HoldingSchemeName);
                        command.Parameters.AddWithValue("@OperationType", holdingSummaryData.OperationType);
                        command.Parameters.AddWithValue("@TotalUnits", holdingSummaryData.TotalUnits);
                        command.Parameters.AddWithValue("@Nav", holdingSummaryData.Nav);
                        command.Parameters.AddWithValue("@Amount", holdingSummaryData.Amount);
                        command.Parameters.AddWithValue("@TransactionDate", holdingSummaryData.TransactionDate);
                        command.Parameters.AddWithValue("@CreatedDate", holdingSummaryData.CreatedDate);
                        command.Parameters.AddWithValue("@ExitDate", holdingSummaryData.ExitDate);

                        affectedTrans = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
            return affectedTrans > 0;

        }

        public bool AllocateFunds(List<TransactionSummaryData> transactions, int userId, decimal navUnits)
        {
            bool isFundsallocated = false;

            decimal balance = _accountTransaction.GetBalance(transactions);
            decimal navAmount = _navAmountPerUnit * navUnits;
            if (balance > _minimumBalance & navUnits >= _minimumUnits & navUnits <= _maximumUnits & navAmount <= balance)
            {
                isFundsallocated = true;
            }

            HoldingSummaryData holdingSummaryData = new HoldingSummaryData
            {
                UniqueId = new Random().Next(5),
                UserId = userId,
                HoldingSchemeName = "Pharma Fund",
                OperationType = "Hold",
                TotalUnits = navUnits,
                Nav = 150,  //Generally it should be retrieved through markets
                Amount = navUnits * 150,    //Generally it should be retrieved through markets
                TransactionDate = new DateTime(2024, 12, 20),
                CreatedDate = new DateTime(2024, 12, 20),
                ExitDate = new DateTime(2026, 12, 20)
            };

            if (isFundsallocated)
            {
                isFundsallocated = AddHolding(holdingSummaryData);
            }

            return isFundsallocated;
        }

        public bool RedeemFunds(List<TransactionSummaryData> transactions, int userId, decimal navUnits)
        {
            bool isFundsRedeemed = false;

            decimal navAmount = _navAmountPerUnit * navUnits;
            if (navUnits >= _minimumUnits & navUnits <= _maximumUnits)
            {
                isFundsRedeemed = true;
            }

            HoldingSummaryData holdingSummaryData = new HoldingSummaryData
            {
                UniqueId = new Random().Next(5),
                UserId = userId,
                HoldingSchemeName = "Pharma Fund",
                OperationType = "Remove",
                TotalUnits = navUnits,
                Nav = 150,  //Generally it should be retrieved through markets
                Amount = navUnits * 150,    //Generally it should be retrieved through markets
                TransactionDate = new DateTime(2024, 12, 20),
                CreatedDate = new DateTime(2024, 12, 20),
                ExitDate = new DateTime(2026, 12, 20)
            };

            if (isFundsRedeemed)
            {
                isFundsRedeemed = AddHolding(holdingSummaryData);
            }

            return isFundsRedeemed;
        }

        
        //svc.AddTransactions();


        public class AccountTransaction
        {
            string _connectionString = string.Empty;

            string _transactionSelectQuery = string.Empty;
            string _transactionInsertQuery = string.Empty;
            string _transactionUpdateQuery = string.Empty;

            decimal _minimumBalance = 0;

            public AccountTransaction()
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

                _transactionSelectQuery = ConfigurationManager.AppSettings["TransactionSelectQuery"];
                _transactionInsertQuery = ConfigurationManager.AppSettings["TransactionInsertQuery"];
                _transactionUpdateQuery = ConfigurationManager.AppSettings["TransactionUpdateQuery"];

                _minimumBalance = Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumBalance"]);
            }

            public decimal GetBalance(List<TransactionSummaryData> transactions)
            {
                decimal depost = 0, withdrawal = 0;
                List<TransactionSummaryData> transactionDeposit = null, transactionsWithdrawal = null;

                transactionDeposit = transactions.Where(t => t.TransactionType == "AD").ToList();
                transactionsWithdrawal = transactions.Where(t => t.TransactionType == "AW").ToList();

                depost = transactionDeposit.Sum(t => t.Amount);
                withdrawal = transactionsWithdrawal.Sum(t => t.Amount);

                //Balance = Deposts - Withdrawals
                return depost - withdrawal;
            }

            public decimal GetDepostAmountOnly(List<TransactionSummaryData> transactions)
            {
                decimal depost = 0;
                List<TransactionSummaryData> transactionDeposit = null;

                transactionDeposit = transactions.Where(t => t.TransactionType == "AD").ToList();

                depost = transactionDeposit.Sum(t => t.Amount);

                //Deposited Amount only
                return depost;
            }

            public decimal GetLowDepostAmountOnly(List<TransactionSummaryData> transactions)
            {
                decimal depost = 0;
                List<TransactionSummaryData> transactionDeposit = null;

                transactionDeposit = transactions.Where(t => t.TransactionType == "AD").ToList();

                depost = transactionDeposit.Min(t => t.Amount);

                //Deposited Amount only
                return depost;
            }

            public decimal GetHighDepostAmountOnly(List<TransactionSummaryData> transactions)
            {
                decimal depost = 0;
                List<TransactionSummaryData> transactionDeposit = null;

                transactionDeposit = transactions.Where(t => t.TransactionType == "AD").ToList();

                depost = transactionDeposit.Max(t => t.Amount);

                //Deposited Amount only
                return depost;
            }

            public decimal GetWithdrawalAmountOnly(List<TransactionSummaryData> transactions)
            {
                decimal withdrawal = 0;
                List<TransactionSummaryData> transactionsWithdrawal = null;

                transactionsWithdrawal = transactions.Where(t => t.TransactionType == "AW").ToList();

                withdrawal = transactionsWithdrawal.Sum(t => t.Amount);

                //Withdrawal Amount only
                return withdrawal;
            }

            public decimal GetLowWithdrawalAmountOnly(List<TransactionSummaryData> transactions)
            {
                decimal withdrawal = 0;
                List<TransactionSummaryData> transactionsWithdrawal = null;

                transactionsWithdrawal = transactions.Where(t => t.TransactionType == "AW").ToList();

                withdrawal = transactionsWithdrawal.Min(t => t.Amount);

                //Withdrawal Amount only
                return withdrawal;
            }

            public decimal GetHighWithdrawalAmountOnly(List<TransactionSummaryData> transactions)
            {
                decimal withdrawal = 0;
                List<TransactionSummaryData> transactionsWithdrawal = null;

                transactionsWithdrawal = transactions.Where(t => t.TransactionType == "AW").ToList();

                withdrawal = transactionsWithdrawal.Max(t => t.Amount);

                //Withdrawal Amount only
                return withdrawal;
            }

            public bool CheckSufficientBalance(List<TransactionSummaryData> transactions)
            {
                decimal balance = GetBalance(transactions);
                return balance >= _minimumBalance;
            }

            public TransactionSummaryResponse GetBalanceWithTransactionSummary(int uid)
            {
                TransactionSummaryResponse response = new TransactionSummaryResponse();
                try
                {
                    List<TransactionSummaryData> transactions = GetTransactionSummary();
                    //Get the transactions of user
                    transactions = transactions.Where(t => t.UserId == uid).ToList();

                    response.TransactionSummary = transactions;
                    response.Amount = GetBalance(transactions);

                }
                catch (Exception ex)//not required to show exception
                {
                    throw ex;
                }
                return response;

            }

            public List<TransactionSummaryData> GetTransactionSummary(int uid)
            {
                List<TransactionSummaryData> transactions = null;
                try
                {
                    transactions = GetTransactionSummary();
                    //Get the transactions of user
                    transactions = transactions.Where(t => t.UserId == uid).ToList();

                }
                catch (Exception ex)//not required to show exception
                {
                    throw ex;
                }
                return transactions;
            }

            public List<GroupingTransactionSummaryData> GetNoOfTransctionsUserWise(List<TransactionSummaryData> transactions)
            {
                // Group transactions by UserId and project into a list of grouped results
                var groupingTransactions = transactions
                    .GroupBy(t => t.UserId)
                    .Select(group => new GroupingTransactionSummaryData
                    {
                        GroupingId = group.Key,
                        TransactionsSummaryData = group.ToList(),
                    })
                    .ToList();
                return groupingTransactions;
            }

            public List<GroupingTransactionSummaryData> GetNoOfDepostTransctionsUserWise(List<TransactionSummaryData> transactions)
            {
                // Group transactions by UserId and project into a list of grouped results
                var groupingTransactions = transactions
                    .Where(t => t.TransactionType == "AD")
                    .GroupBy(t => t.UserId)
                    .Select(group => new GroupingTransactionSummaryData
                    {
                        GroupingId = group.Key,
                        TransactionsSummaryData = group.ToList(),
                    })
                    .ToList();
                return groupingTransactions;
            }

            public List<GroupingTransactionSummaryData> GetNoOfWithdrawalsTransctionsUserWise(List<TransactionSummaryData> transactions)
            {
                // Group transactions by UserId and project into a list of grouped results
                var groupingTransactions = transactions
                    .Where(t => t.TransactionType == "AW")
                    .GroupBy(t => t.UserId)
                    .Select(group => new GroupingTransactionSummaryData
                    {
                        GroupingId = group.Key,
                        TransactionsSummaryData = group.ToList(),
                    })
                    .ToList();
                return groupingTransactions;
            }

            public List<GroupingTransactionSummaryData> GetNoOfMinimumDepostTransctionsUserWise(List<TransactionSummaryData> transactions)
            {
                decimal minimumDepostAmount = 500;

                // Group transactions by UserId and project into a list of grouped results
                var groupingTransactions = transactions
                    .Where(t => t.TransactionType == "AD" & t.Amount <= minimumDepostAmount)
                    .GroupBy(t => t.UserId)
                    .Select(group => new GroupingTransactionSummaryData
                    {
                        GroupingId = group.Key,
                        TransactionsSummaryData = group.ToList(),
                    })
                    .ToList();
                return groupingTransactions;
            }

            public List<GroupingTransactionSummaryData> GetNoOfMaximumDepostTransctionsUserWise(List<TransactionSummaryData> transactions)
            {
                decimal minimumDepostAmount = 500;
                decimal maximumDepostAmount = 3500;

                // Group transactions by UserId and project into a list of grouped results
                var groupingTransactions = transactions
                    .Where(t => t.TransactionType == "AD" & t.Amount >= minimumDepostAmount & t.Amount <= maximumDepostAmount)
                    .GroupBy(t => t.UserId)
                    .Select(group => new GroupingTransactionSummaryData
                    {
                        GroupingId = group.Key,
                        TransactionsSummaryData = group.ToList(),
                    })
                    .ToList();
                return groupingTransactions;
            }

            public List<GroupingTransactionSummaryData> GetNoOfMinimumWithdrawalTransctionsUserWise(List<TransactionSummaryData> transactions)
            {
                decimal minimumWithdrawalAmount = 500;

                // Group transactions by UserId and project into a list of grouped results
                var groupingTransactions = transactions
                    .Where(t => t.TransactionType == "AW" & t.Amount <= minimumWithdrawalAmount)
                    .GroupBy(t => t.UserId)
                    .Select(group => new GroupingTransactionSummaryData
                    {
                        GroupingId = group.Key,
                        TransactionsSummaryData = group.ToList(),
                    })
                    .ToList();
                return groupingTransactions;
            }

            public List<GroupingTransactionSummaryData> GetNoOfMaximumWithdrawalTransctionsUserWise(List<TransactionSummaryData> transactions)
            {
                decimal minimumDepostAmount = 500;
                decimal maximumDepostAmount = 3500;

                // Group transactions by UserId and project into a list of grouped results
                var groupingTransactions = transactions
                    .Where(t => t.TransactionType == "AW" & t.Amount >= minimumDepostAmount & t.Amount <= maximumDepostAmount)
                    .GroupBy(t => t.UserId)
                    .Select(group => new GroupingTransactionSummaryData
                    {
                        GroupingId = group.Key,
                        TransactionsSummaryData = group.ToList(),
                    })
                    .ToList();
                return groupingTransactions;
            }

            public List<TransactionSummaryData> GetTransactionalReportByDatesRange(List<TransactionSummaryData> transactions, DateTime startDate, DateTime endDate)
            {
                // Group transactions for given dates range
                transactions = transactions.Where(t => t.TransactionDate >= startDate & t.TransactionDate <= endDate).ToList();
                return transactions;
            }

            public List<TransactionSummaryData> GetTransactionSummary()
            {
                List<TransactionSummaryData> transactions = new List<TransactionSummaryData>();
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(_transactionSelectQuery, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Use the GetList<T> method to generate List of objects
                                transactions = reader.GetList<TransactionSummaryData>();
                            }
                        }
                    }
                }
                catch (Exception ex)//not required to show exception
                {
                    throw ex;
                }
                return transactions;

            }

            public bool AddTransactions()
            {
                bool isaffectedTransactions = false;

                // Transactions to insert
                var transactions = new List<TransactionSummaryData>
            {
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10001, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 1500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10001, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is Withdrawn", Amount = 500, TransactionType="AW" },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10001, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 2500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10002, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 3500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10003, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 4500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10005, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 5500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10006, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10007, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10008, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10009, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10010, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10007, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is withdrawn", Amount = 6500, TransactionType="AW"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10008, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is withdrawn", Amount = 6500, TransactionType="AW"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10009, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is withdrawn", Amount = 6500, TransactionType="AW"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10010, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is withdrawn", Amount = 6500, TransactionType="AW"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10007, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10008, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10009, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = new Random().Next(5), UserId = 10010, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
            };

                // Insert transactions into the database
                foreach (var transaction in transactions)
                {
                    isaffectedTransactions = AddTransaction(transaction);
                }
                return isaffectedTransactions;
            }

            public bool AddTransaction(TransactionSummaryData transactionSummaryData)
            {
                int affectedTrans = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        // Open the connection and execute the query
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(_transactionInsertQuery, connection))
                        {
                            // Add parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@UniqueId", transactionSummaryData.UniqueId);
                            command.Parameters.AddWithValue("@UserId", transactionSummaryData.UserId);
                            command.Parameters.AddWithValue("@TransactionDate", transactionSummaryData.TransactionDate);
                            command.Parameters.AddWithValue("@Description", transactionSummaryData.Description);
                            command.Parameters.AddWithValue("@Amount", transactionSummaryData.Amount);
                            command.Parameters.AddWithValue("@TransactionType", transactionSummaryData.TransactionType);

                            affectedTrans = command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)//not required to show exception
                {
                    throw ex;
                }
                return affectedTrans > 0;

            }

            public bool UpdateTransaction(TransactionSummaryData transactionSummaryData)
            {
                int affectedTrans = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        // Open the connection and execute the query
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(_transactionUpdateQuery, connection))
                        {
                            // Add parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@UniqueId", transactionSummaryData.UniqueId);
                            command.Parameters.AddWithValue("@UserId", transactionSummaryData.UserId);
                            command.Parameters.AddWithValue("@TransactionDate", transactionSummaryData.TransactionDate);
                            command.Parameters.AddWithValue("@Description", transactionSummaryData.Description);
                            command.Parameters.AddWithValue("@Amount", transactionSummaryData.Amount);
                            command.Parameters.AddWithValue("@TransactionType", transactionSummaryData.TransactionType);

                            affectedTrans = command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)//not required to show exception
                {
                    throw ex;
                }
                return affectedTrans > 0;

            }

        }
    }

}
