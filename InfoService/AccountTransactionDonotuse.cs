using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContractLibrary;

namespace AccountInfo_donotuse
{
    public class AccountTransaction_donotuse
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
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10001, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 1500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10001, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is Withdrawn", Amount = 500, TransactionType="AW" },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10001, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 2500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10002, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 3500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10003, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 4500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10005, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 5500, TransactionType="AD" },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10006, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10007, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10008, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10009, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10010, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10007, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is withdrawn", Amount = 6500, TransactionType="AW"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10008, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is withdrawn", Amount = 6500, TransactionType="AW"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10009, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is withdrawn", Amount = 6500, TransactionType="AW"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10010, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is withdrawn", Amount = 6500, TransactionType="AW"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10007, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10008, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10009, TransactionDate=new DateTime(2024, 12, 20),
                    Description = "Amount is deposited", Amount = 6500, TransactionType="AD"  },
                new TransactionSummaryData { UniqueId = UniqueIdGenerator.GetUniqueId(), UserId = 10010, TransactionDate=new DateTime(2024, 12, 20),
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
