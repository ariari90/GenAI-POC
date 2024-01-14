using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RequestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IWithdrawService
    {
        string _connectionString = @"Data Source=DESKTOP-2PDJ9M3; Database=gen_ai_poc; Initial Catalog=gen_ai_poc; Integrated Security=True";
        int _amountPerUnits = 10;

        public void WithdrawT1Amount(int uniqueId, string product, decimal withdrawPercent)
        {
            InfoService.IAccountBankingService bankingRequest = new InfoService.AccountBankingService();
            var holdingSummaryResponse = bankingRequest.GetHoldingSummary(uniqueId);
            var holdingForScheme = holdingSummaryResponse.HoldingSummaryData.Where(x => x.SchemeName == product).FirstOrDefault();

            // Calculate new amounts
            int newTotalUnits = (int)Math.Floor(holdingForScheme.TotalUnits - (holdingForScheme.TotalUnits / 100 * withdrawPercent));
            decimal newAmount = newTotalUnits * _amountPerUnits;
            decimal withdrewAmount = (holdingForScheme.TotalUnits / 100 * withdrawPercent);

            if (newTotalUnits < 0)
            {
                throw new Exception("Units cannot be less than zero");
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
        }

        public string ExitRequest(int uniqueId, string schemeName)
        {
            string status = String.Empty;

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
                    status = "Success";
                }
                else
                {
                    status = "Failure";
                }

                connection.Close();
            }
            return status;
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
