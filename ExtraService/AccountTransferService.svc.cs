using Common;
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
    public class AccountTransferService : IAccountTransferService
    {
        private readonly string _connectionString;
        private readonly int _minPaymentAmount;
        private readonly string _fundManagerName;
        private readonly string _fundBankBranch;
        private readonly decimal _navAmount;
        private readonly decimal _interestRate;

        public AccountTransferService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
            _minPaymentAmount = Convert.ToInt32(ConfigurationManager.AppSettings["MinPaymentAmount"]);
            _fundManagerName = ConfigurationManager.AppSettings["FundManagerName"];
            _fundBankBranch = ConfigurationManager.AppSettings["FundBankBranch"];
            _navAmount = Convert.ToInt32(ConfigurationManager.AppSettings["NavAmount"]);
            _interestRate = Convert.ToInt32(ConfigurationManager.AppSettings["InterestRate"]);
        }

        public AccountTransferSvcResponse Execute(AccountTransferSvcRequest workflowRequest)
        {
            AccountTransferSvcResponse workflowResponse = new AccountTransferSvcResponse();
            TransactionSummaryData transactionSummaryData = new TransactionSummaryData();
            // 1. Create a Personal Fund Account
            CreatePersonalFundAccount(workflowRequest.AccountNumberUniqueId, workflowRequest.NewAccountNumber, "BankName");

            // 2. Create a Business Fund Account
            CreateBusinessFundAccount(workflowRequest.BusinessAccountUniqueId, workflowRequest.BusinessAccountUniqueId, "BankName");

            // 3. Deposit Funds
            DepositFunds(workflowRequest.AccountNumberUniqueId, 1000m);

            // 4. Withdraw Funds
            WithdrawFunds(workflowRequest.AccountNumberUniqueId, 500m);

            // 5. Transfer Funds
            TransferFunds(workflowRequest.AccountNumberUniqueId, workflowRequest.BusinessAccountUniqueId, 200m);

            // 6. Start Scheduled Payments
            StartScheduledPayments(workflowRequest.AccountNumberUniqueId, 100m);

            // 7. Apply for a Loan
            ApplyForLoan(workflowRequest.AccountNumberUniqueId, 5000m, "Personal Loan", 12);

            // 8. Recalculate and Apply Interest to All Loans
            RecalculateAndApplyInterestToAllLoans(5.0m);

            // 9. Pay Loan Installments
            PayLoanInstallments(workflowRequest.AccountNumberUniqueId, 1, 500m);

            // 10. Apply for a Credit Card
            ApplyForCreditCard(workflowRequest.AccountNumberUniqueId, "Gold", 10000m);

            // 11. Generate Account Statement
            List<TransactionSummaryData> transactions = GenerateAccountStatement(workflowRequest.AccountNumberUniqueId, DateTime.Now.AddYears(-80), DateTime.Now);
            workflowResponse.TransactionSummaryData = new List<TransactionSummaryData>();
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.TransactionDate}: {transaction.Description} - {transaction.Amount}");
                workflowResponse.TransactionSummaryData.Add(transaction);
            }

            // 12. Cancel Scheduled Payments
            CancelScheduledPayments(1);

            // 13. Generate Loan Statement
            List<LoanPaymentData> loanPayments = GenerateLoanStatement(workflowRequest.AccountNumberUniqueId, DateTime.Now.AddYears(-80), DateTime.Now);
            workflowResponse.LoanPaymentData = new List<LoanPaymentData>();
            foreach (var payment in loanPayments)
            {
                Console.WriteLine($"{payment.PaymentDate}: Payment of {payment.PaymentAmount}");
                workflowResponse.LoanPaymentData.Add(payment);
            }

            // 14. Close Account
            CloseAccount(1);
            return workflowResponse;
        }

        public void TransferFunds(int uniqueIdFrom, int uniqueIdTo, decimal amount)
        {
            if (uniqueIdFrom == uniqueIdTo)
            {
                throw new Exception("Error: Cannot transfer funds to the same account.");
            }

            decimal amountFrom = 0;
            decimal amountTo = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select Amount, UniqueId from HoldingSummary where uniqueId in (@UniqueId1, @UniqueId2) and HoldingSchemeName=@HoldingSchemeName", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId1", uniqueIdFrom);
                        command.Parameters.AddWithValue("@UniqueId2", uniqueIdTo);
                        command.Parameters.AddWithValue("@HoldingSchemeName", "FundScheme");
                        var reader = command.ExecuteReader();

                        int uidCounts = 0;

                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["UniqueId"]) == uniqueIdFrom)
                            {
                                amountFrom = Convert.ToInt32(reader["Amount"]);
                            }
                            else if (Convert.ToInt32(reader["UniqueId"]) == uniqueIdTo)
                            {
                                amountTo = Convert.ToInt32(reader["Amount"]);
                            }
                            uidCounts++;
                        }

                        if (uidCounts != 2)
                        {
                            throw new Exception("Error: UniqueIds must exist");
                        }
                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }

            // Transfer funds
            decimal newAmountFrom = amountFrom - amount;
            decimal newAmountTo = amountTo + amount;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection and execute the query
                connection.Open();
                using (SqlCommand command = new SqlCommand("update HoldingSummary set Amount=@NewAmount where uniqueId = @UniqueIdFrom and holdingSchemeName=@holdingSchemeName", connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@UniqueIdFrom", uniqueIdFrom);
                    command.Parameters.AddWithValue("@NewAmount", newAmountFrom);
                    command.Parameters.AddWithValue("@holdingSchemeName", "FundScheme");
                    command.ExecuteNonQuery();
                }
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("update HoldingSummary set Amount=@NewAmount where uniqueId = @UniqueIdTo and HoldingSchemeName=@holdingSchemeName", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueIdTo", uniqueIdTo);
                        command.Parameters.AddWithValue("@NewAmount", newAmountTo);
                        command.Parameters.AddWithValue("@holdingSchemeName", "FundScheme");
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO [dbo].[TransactionSummary]
                           ([UniqueId]
                           ,[UserId]
                           ,[TransactionDate]
                           ,[Description]
                           ,[Amount]
                           ,[TransactionType])
                     VALUES
                           (@UniqueId
                           ,@UserId
                           ,@TransactionDate
                           ,@Description
                           ,@Amount
                           ,@TransactionType)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        string description = String.Format("Transfered amount {0} to account {1}", amount, uniqueIdTo);
                        command.Parameters.AddWithValue("@UniqueId", uniqueIdTo);
                        command.Parameters.AddWithValue("@UserId", uniqueIdTo);
                        command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@TransactionType", "TR");
                        command.ExecuteNonQuery();
                    }

                    // Insert into TaxableEvents
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO TaxableEvents 
                            (UniqueId, EventDate, Description, Amount, TaxType) 
                            VALUES (@UniqueId, @EventDate, @Description, @Amount, @TaxType)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueIdFrom);
                        command.Parameters.AddWithValue("@EventDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Description", "Transfer");
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@TaxType", "Transfer");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }

            return;
        }


        public void CreatePersonalFundAccount(int uniqueId, int accountNumber, string bankName)
        {
            //"select uniqueId, accountNumber, bankBranch, bankName, address, ifscCode from BankInfo where uniqueId=" + uniqueId, conn);

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(@"insert into Bankinfo
                            (UniqueId, BankBranch, BankName, Address, ifscCode) values(
                            @UniqueId, 
                            @BankBranch,
                            @BankName, 
                            @Address, 
                            @ifscCode)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@bankBranch", _fundBankBranch);
                        command.Parameters.AddWithValue("@bankName", bankName);
                        command.Parameters.AddWithValue("@address", "Address1");
                        command.Parameters.AddWithValue("@ifscCode", 123);

                        command.ExecuteNonQuery();
                    }

                    //select uniqueId, schemeId, schemeName, fundManagerName, percantageContribution, createdDate, exitDate, isPreferred from SchemeInfo where uniqueId =
                    using (SqlCommand command = new SqlCommand(@"insert into SchemeInfo 
                        (uniqueId, schemeName, fundManagerName, percantageContribution, createdDate, isPreferred) 
                        values (@uniqueId, 
                        @schemeName, 
                        @fundManagerName, 
                        @percantageContribution, 
                        @createdDate, 
                        @isPreferred)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@schemeName", "FundScheme");
                        command.Parameters.AddWithValue("@fundManagerName", _fundManagerName);
                        command.Parameters.AddWithValue("@percantageContribution", 10);
                        command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                        command.Parameters.AddWithValue("@isPreferred", true);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
        }

        public void CreateBusinessFundAccount(int uniqueId, int accountNumber, string bankName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(@"insert into Bankinfo 
                        (UniqueId, BankBranch, BankName, Address, ifscCode) values(
                        @UniqueId, 
                        @BankBranch, 
                        @BankName, 
                        @Address, 
                        @ifscCode)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@bankBranch", _fundBankBranch);
                        command.Parameters.AddWithValue("@bankName", bankName);
                        command.Parameters.AddWithValue("@address", "Address1");
                        command.Parameters.AddWithValue("@ifscCode", 123);

                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(@"insert into SchemeInfo 
                        (uniqueId, schemeName, fundManagerName, percantageContribution, createdDate, isPreferred) 
                        values (
                        @uniqueId, 
                        @schemeName, 
                        @fundManagerName, 
                        @percantageContribution, 
                        @createdDate, 
                        @isPreferred)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@schemeName", "BUSINESS");
                        command.Parameters.AddWithValue("@fundManagerName", _fundManagerName);
                        command.Parameters.AddWithValue("@percantageContribution", 10);
                        command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                        command.Parameters.AddWithValue("@isPreferred", true);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
        }

        public void CloseAccount(int uniqueId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("update SchemeInfo set exitDate=@ExitDate where uniqueId=@UniqueId and SchemeName='FundScheme'", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@ExitDate", DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
        }

        public void StartScheduledPayments(int uniqueId, decimal amount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select Amount, UniqueId from HoldingSummary where uniqueId = @UniqueId and HoldingSchemeName=@HoldingSchemeName", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@HoldingSchemeName", "FundScheme");
                        var reader = command.ExecuteReader();


                        if (reader.Read())
                        {
                            int holdingAmount = Convert.ToInt32(reader["Amount"]);
                            if (holdingAmount < _minPaymentAmount)
                            {
                                throw new Exception("Scheduled payment service is not available: Insufficient amount.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO [dbo].[SchemeInfo]
                           ([schemeName]
                           ,[fundManagerName]
                           ,[percantageContribution]
                           ,[uniqueId]
                           ,[status]
                           ,[CreatedDate]
                           ,[IsPreferred])
                     VALUES
                           (@schemeName
                           ,@fundManagerName
                           ,@percantageContribution
                           ,@uniqueId
                           ,@status
                           ,@CreatedDate
                           ,@IsPreferred)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@schemeName", "SCH_PMNTS");
                        command.Parameters.AddWithValue("@fundManagerName", _fundManagerName);
                        command.Parameters.AddWithValue("@percantageContribution", 45);
                        command.Parameters.AddWithValue("@uniqueId", uniqueId);
                        command.Parameters.AddWithValue("@status", "Active");
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@IsPreferred", false);
                        var reader = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)//not required to show exception
            {
                throw ex;
            }
        }

        public void CancelScheduledPayments(int uniqueId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE SchemeInfo SET Status = 'Cancelled' WHERE UniqueId = @UniqueId AND SchemeName = 'SCH_PMNTS'", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }
        }

        public void ApplyForLoan(int uniqueId, decimal loanAmount, string loanType, int loanTermMonths)
        {
            decimal holdingAmount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select Amount from HoldingSummary where uniqueId = @UniqueId and HoldingSchemeName=@HoldingSchemeName", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@HoldingSchemeName", "FundScheme");
                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            holdingAmount = Convert.ToDecimal(reader["Amount"]);
                        }
                        else
                        {
                            throw new Exception("Error: UniqueId does not exist in HoldingSummary.");
                        }
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }

            if (holdingAmount < loanAmount)
            {
                throw new Exception("Error: Insufficient holdings to apply for the loan.");
            }

            // Calculate total repayment amount with interest
            decimal monthlyInterestRate = _interestRate / 12 / 100;
            decimal totalRepaymentAmount = loanAmount * (decimal)Math.Pow((double)(1 + monthlyInterestRate), loanTermMonths);

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO LoanApplications 
                            (UniqueId, LoanAmount, LoanType, ApplicationDate, Status, InterestRate, LoanTermMonths, TotalRepaymentAmount) 
                            VALUES (
                            @UniqueId, 
                            @LoanAmount, 
                            @LoanType, 
                            @ApplicationDate, 
                            @Status, 
                            @InterestRate, 
                            @LoanTermMonths, 
                            @TotalRepaymentAmount)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@LoanAmount", loanAmount);
                        command.Parameters.AddWithValue("@LoanType", loanType);
                        command.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Status", "Pending");
                        command.Parameters.AddWithValue("@InterestRate", _interestRate);
                        command.Parameters.AddWithValue("@LoanTermMonths", loanTermMonths);
                        command.Parameters.AddWithValue("@TotalRepaymentAmount", totalRepaymentAmount);

                        command.ExecuteNonQuery();
                    }

                    // Insert into TaxableEvents
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO TaxableEvents 
                            (UniqueId, EventDate, Description, Amount, TaxType) 
                            VALUES (@UniqueId, @EventDate, @Description, @Amount, @TaxType)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@EventDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Description", "Loan Application");
                        command.Parameters.AddWithValue("@Amount", loanAmount);
                        command.Parameters.AddWithValue("@TaxType", "Loan");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }
        }

        public void RecalculateAndApplyInterestToAllLoans(decimal newInterestRate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT Id, UniqueId, LoanAmount, LoanTermMonths FROM LoanApplications WHERE Status = 'Pending'", connection))
                    {
                        var reader = command.ExecuteReader();
                        var loansToUpdate = new List<LoanData>();

                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["Id"]);
                            int uniqueId = Convert.ToInt32(reader["UniqueId"]);
                            decimal loanAmount = Convert.ToDecimal(reader["LoanAmount"]);
                            int loanTermMonths = Convert.ToInt32(reader["LoanTermMonths"]);
                            LoanData loan = new LoanData
                            {
                                Id = id,
                                UniqueId = uniqueId,
                                LoanAmount = loanAmount,
                                LoanTermMonths = loanTermMonths,
                            };

                            loansToUpdate.Add(loan);
                        }

                        reader.Close();

                        foreach (var loan in loansToUpdate)
                        {
                            decimal monthlyInterestRate = newInterestRate / 12 / 100;
                            decimal totalRepaymentAmount = loan.LoanAmount * (decimal)Math.Pow((double)(1 + monthlyInterestRate), loan.LoanTermMonths);

                            using (SqlCommand updateCommand = new SqlCommand(@"UPDATE LoanApplications 
                                                SET InterestRate = @InterestRate, 
                                                TotalRepaymentAmount = 
                                                @TotalRepaymentAmount WHERE Id = @Id", connection))
                            {
                                updateCommand.Parameters.AddWithValue("@InterestRate", newInterestRate);
                                updateCommand.Parameters.AddWithValue("@TotalRepaymentAmount", totalRepaymentAmount);
                                updateCommand.Parameters.AddWithValue("@Id", loan.Id);

                                updateCommand.ExecuteNonQuery();
                            }

                            // Insert into TaxableEvents
                            using (SqlCommand taxableEventsCommand = new SqlCommand(@"INSERT INTO TaxableEvents 
                                    (UniqueId, EventDate, Description, Amount, TaxType) 
                                    VALUES (@UniqueId, @EventDate, @Description, @Amount, @TaxType)", connection))
                            {
                                // Add parameters to prevent SQL injection
                                taxableEventsCommand.Parameters.AddWithValue("@UniqueId", loan.UniqueId);
                                taxableEventsCommand.Parameters.AddWithValue("@EventDate", DateTime.Now);
                                taxableEventsCommand.Parameters.AddWithValue("@Description", "Interest Recalculation");
                                taxableEventsCommand.Parameters.AddWithValue("@Amount", totalRepaymentAmount - loan.LoanAmount);
                                taxableEventsCommand.Parameters.AddWithValue("@TaxType", "Interest");

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }
        }

        public void DepositFunds(int uniqueId, decimal amount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE HoldingSummary SET Amount = Amount + @Amount WHERE UniqueId = @UniqueId AND HoldingSchemeName = @HoldingSchemeName", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@HoldingSchemeName", "FundScheme");

                        command.ExecuteNonQuery();
                    }

                    // Insert into TaxableEvents
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO TaxableEvents 
                            (UniqueId, EventDate, Description, Amount, TaxType) 
                            VALUES (@UniqueId, @EventDate, @Description, @Amount, @TaxType)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@EventDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Description", "Deposit");
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@TaxType", "Deposit");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }
        }

        public void WithdrawFunds(int uniqueId, decimal amount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE HoldingSummary SET Amount = Amount - @Amount WHERE UniqueId = @UniqueId AND HoldingSchemeName = @HoldingSchemeName", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@HoldingSchemeName", "FundScheme");

                        command.ExecuteNonQuery();
                    }

                    // Insert into TaxableEvents
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO TaxableEvents 
                            (UniqueId, EventDate, Description, Amount, TaxType) 
                            VALUES (@UniqueId, @EventDate, @Description, @Amount, @TaxType)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@EventDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Description", "Withdrawal");
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@TaxType", "Withdrawal");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }
        }

        public decimal CheckAccountBalance(int uniqueId)
        {
            decimal balance = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT Amount FROM HoldingSummary WHERE UniqueId = @UniqueId AND HoldingSchemeName = @HoldingSchemeName", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@HoldingSchemeName", "FundScheme");

                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            balance = Convert.ToDecimal(reader["Amount"]);
                        }
                        else
                        {
                            throw new Exception("Error: UniqueId does not exist in HoldingSummary.");
                        }
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }

            return balance;
        }

        public void ApplyForCreditCard(int uniqueId, string cardType, decimal creditLimit)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO CreditCardApplications 
                            (UniqueId, CardType, CreditLimit, ApplicationDate, Status) 
                            VALUES (@UniqueId, @CardType, @CreditLimit, @ApplicationDate, @Status)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@CardType", cardType);
                        command.Parameters.AddWithValue("@CreditLimit", creditLimit);
                        command.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Status", "Pending");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }
        }

        public void PayLoanInstallments(int uniqueId, int loanId, decimal paymentAmount)
        {
            decimal remainingAmount = 0;
            decimal totalRepaymentAmount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT TotalRepaymentAmount FROM LoanApplications WHERE Id = @LoanId AND UniqueId = @UniqueId AND Status = 'Pending'", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@LoanId", loanId);
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            totalRepaymentAmount = Convert.ToDecimal(reader["TotalRepaymentAmount"]);
                        }
                        else
                        {
                            throw new Exception("Error: Loan not found or already paid off.");
                        }
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }

            remainingAmount = totalRepaymentAmount - paymentAmount;

            if (remainingAmount < 0)
            {
                throw new Exception("Error: Payment amount exceeds the remaining loan balance.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE LoanApplications SET TotalRepaymentAmount = @RemainingAmount, Status = CASE WHEN @RemainingAmount = 0 THEN 'Paid' ELSE 'Pending' END WHERE Id = @LoanId AND UniqueId = @UniqueId", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@RemainingAmount", remainingAmount);
                        command.Parameters.AddWithValue("@LoanId", loanId);
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);

                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(@"INSERT INTO LoanPayments 
                            (LoanId, UniqueId, PaymentAmount, PaymentDate) 
                            VALUES (@LoanId, @UniqueId, @PaymentAmount, @PaymentDate)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@LoanId", loanId);
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@PaymentAmount", paymentAmount);
                        command.Parameters.AddWithValue("@PaymentDate", DateTime.Now);

                        command.ExecuteNonQuery();
                    }

                    // Insert into TaxableEvents
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO TaxableEvents 
                            (UniqueId, EventDate, Description, Amount, TaxType) 
                            VALUES (@UniqueId, @EventDate, @Description, @Amount, @TaxType)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@EventDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Description", "Loan Payment");
                        command.Parameters.AddWithValue("@Amount", paymentAmount);
                        command.Parameters.AddWithValue("@TaxType", "Loan Payment");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }
        }

        public List<TransactionSummaryData> GenerateAccountStatement(int uniqueId, DateTime startDate, DateTime endDate)
        {
            var transactions = new List<TransactionSummaryData>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM TransactionSummary WHERE UniqueId = @UniqueId AND TransactionDate BETWEEN @StartDate AND @EndDate ORDER BY TransactionDate DESC", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var transaction = new TransactionSummaryData
                            {
                                TransactionSummaryId = Convert.ToInt32(reader["TransactionSummaryId"]),
                                UniqueId = Convert.ToInt32(reader["UniqueId"].ToString()),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                                Description = reader["Description"].ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                TransactionType = reader["TransactionType"].ToString()
                            };

                            transactions.Add(transaction);
                        }
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }

            return transactions;
        }

        public List<LoanPaymentData> GenerateLoanStatement(int loanId, DateTime startDate, DateTime endDate)
        {
            var loanPayments = new List<LoanPaymentData>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM LoanPayments WHERE LoanId = @LoanId AND PaymentDate BETWEEN @StartDate AND @EndDate ORDER BY PaymentDate DESC", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@LoanId", loanId);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var loanPayment = new LoanPaymentData
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                LoanId = Convert.ToInt32(reader["LoanId"]),
                                UniqueId = Convert.ToInt32(reader["UniqueId"]),
                                PaymentAmount = Convert.ToDecimal(reader["PaymentAmount"]),
                                PaymentDate = Convert.ToDateTime(reader["PaymentDate"])
                            };

                            loanPayments.Add(loanPayment);
                        }
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }

            return loanPayments;
        }

        public void SetUpAutomaticSavingsPlan(int uniqueId, decimal amount, string savingsAccountNumber, int intervalDays)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO AutomaticSavingsPlan 
                            (UniqueId, Amount, SavingsAccountNumber, IntervalDays, StartDate, Status) 
                            VALUES (@UniqueId, @Amount, @SavingsAccountNumber, @IntervalDays, @StartDate, @Status)", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@SavingsAccountNumber", savingsAccountNumber);
                        command.Parameters.AddWithValue("@IntervalDays", intervalDays);
                        command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Status", "Active");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }
        }

        public List<TaxReportData> GenerateTaxReport(int uniqueId, DateTime startDate, DateTime endDate)
        {
            var taxReport = new List<TaxReportData>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection and execute the query
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM TaxableEvents WHERE UniqueId = @UniqueId AND EventDate BETWEEN @StartDate AND @EndDate ORDER BY EventDate DESC", connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@UniqueId", uniqueId);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var taxEvent = new TaxReportData
                            {
                                EventId = Convert.ToInt32(reader["EventId"]),
                                UniqueId = Convert.ToInt32(reader["UniqueId"]),
                                EventDate = Convert.ToDateTime(reader["EventDate"]),
                                Description = reader["Description"].ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                TaxType = reader["TaxType"].ToString()
                            };

                            taxReport.Add(taxEvent);
                        }
                    }
                }
            }
            catch (Exception ex) // not required to show exception
            {
                throw ex;
            }

            return taxReport;
        }
    }

    public class LoanData
    {
        public int Id;
        public int UniqueId;
        public decimal LoanAmount;
        public int LoanTermMonths;
    }

    public class TaxReportData
    {
        public int EventId;
        public int UniqueId;
        public DateTime EventDate;
        public string Description;
        public decimal Amount;
        public string TaxType;
    };

    public class AccountTransfersWorkflow
    {
        
    }
}
