using Common.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RequestService
{
    public class WithdrawService : IWithdrawService
    {
        string _connectionString;
        int _amountPerUnits = 10;

        public WithdrawService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
        }

        public ValidationResponse WithdrawT1Amount(int uniqueId, string product, decimal withdrawPercent)
        {
            ValidationResponse response = new ValidationResponse();

            InfoService.IAccountBankingService bankingRequest = new InfoService.AccountBankingService();
            var holdingSummaryResponse = bankingRequest.GetHoldingSummary(uniqueId);
            var holdingForScheme = holdingSummaryResponse.HoldingSummaryData.Where(x => x.HoldingSchemeName == product).FirstOrDefault();

            if (holdingForScheme == null)
            {
                response.Status = "Fail";
                response.ValidationMessage = "No holding found for user."; 
                return response;
            }

            // Calculate new amounts
            int newTotalUnits = (int)Math.Floor(holdingForScheme.TotalUnits - (holdingForScheme.TotalUnits / 100 * withdrawPercent));
            decimal newAmount = newTotalUnits * _amountPerUnits;
            decimal withdrewAmount = (holdingForScheme.TotalUnits / 100 * withdrawPercent);

            if (newTotalUnits < 0)
            {
                response.Status = "Fail";
                response.ValidationMessage = "Units cannot be less than zero";
                return response;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();

                // Update Holding Summary
                command.CommandText = @"UPDATE HoldingSummary set TotalUnits=@TotalUnits, Amount=@Amount
                                    WHERE uniqueId = " + uniqueId +
                            " AND schemeName='" + product + "'";

                command.Parameters.AddWithValue("@TotalUnits", newTotalUnits);
                command.Parameters.AddWithValue("@Amount", newAmount);

                command.ExecuteNonQuery();

                // Update Transaction
                command.CommandText = @"insert into TransactionSummary values (@UniqueId, @TransactionDate, @Description, @Amount_Withdrew, @TransactionType)";

                command.Parameters.AddWithValue("@UniqueId", uniqueId);
                command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                command.Parameters.AddWithValue("@Description", "Withdrew " + withdrawPercent + "%");
                command.Parameters.AddWithValue("@Amount_Withdrew", withdrewAmount);
                command.Parameters.AddWithValue("@TransactionType", "W");

                command.ExecuteNonQuery();
                connection.Close();
            }
            response.Status = "Success";
            return response;
        }

        public ValidationResponse ExitRequest(int uniqueId, string schemeName)
        {

            ValidationResponse response = new ValidationResponse();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE SchemeInfo set Status=@Status, ExitDate=@ExitDate 
                                 WHERE uniqueId = " + uniqueId + 
                             " AND schemeName='" + schemeName + "'";

                command.Parameters.AddWithValue("@Status", "Reject");
                command.Parameters.AddWithValue("@ExitDate", DateTime.Now);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    command.CommandText = @"UPDATE HoldingSummary set ExitDate=@Sum_ExitDate 
                               WHERE uniqueId = " + uniqueId +
                             " AND schemeName='" + schemeName + "'";

                    command.Parameters.AddWithValue("@Sum_ExitDate", DateTime.Now);
                    command.ExecuteNonQuery();
                    response.Status = "Success";
                }
                else
                {
                    response.Status = "Failure";
                    response.ValidationMessage = "No records are updated for withdrawal";
                }

                connection.Close();
            }
            response.Status = "Success";
            return response;
        }

        public ExitRequestResponse GetExitStatus (int uniqueId, string schemeName)
        {
            ExitRequestResponse response = new ExitRequestResponse();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"select ExitDate, Status from SchemeInfo WHERE uniqueId = " + uniqueId +
                             " AND schemeName='" + schemeName + "'", connection);

                var dataReader = cmd.ExecuteReader();

                if(dataReader.Read())
                {
                    if(dataReader["ExitDate"] == null || String.IsNullOrEmpty(dataReader["ExitDate"].ToString()))
                    {
                        response.Status = "No Request Raised";
                    }
                    else
                    {
                        string test = dataReader["ExitDate"].ToString();
                        response.Status = dataReader["Status"].ToString();
                        response.DateRaised = Convert.ToDateTime(dataReader["ExitDate"]);
                    }
                }
                else
                {
                    response.Status = "No Request Raised";
                }
                connection.Close();
            }
            return response;
        }

    }
}
