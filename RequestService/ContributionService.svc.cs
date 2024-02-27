using Common.Entities;
using InfoService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RequestService
{
    public class ContributionService : IContributionService
    {
        private readonly string _connectionString;
        private int _amountPerUnits = 10;

        public ContributionService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
        }

        public ValidationResponse ContributeOnline(int uniqueId, string product, int units)
        {
            ValidationResponse response = new ValidationResponse();
            if (units <= 0)
            {
                response.Status = "Fail";
                response.ValidationMessage = "You cannot contribute 0 units";
                return response;
            }

            InfoService.IAccountBankingService bankingRequest = new InfoService.AccountBankingService();
            var holdingSummaryResponse = bankingRequest.GetHoldingSummary(uniqueId);
            var holdingForScheme = holdingSummaryResponse.HoldingSummaryData.Where(x => x.HoldingSchemeName == product).FirstOrDefault();

            if (holdingForScheme == null)
            {
                response.Status = "Fail";
                response.ValidationMessage = "Scheme not found for user";
                return response;
            }

            // Calculate new amounts
            int newTotalUnits = (int)Math.Floor(holdingForScheme.TotalUnits + units);
            decimal newAmount = newTotalUnits * _amountPerUnits;
            decimal contributedAmount = units * _amountPerUnits;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();

                // Update Holding Summary
                command.CommandText = @"UPDATE HoldingSummary set TotalUnits=@TotalUnits, Amount=@Amount
                                    WHERE uniqueId = " + uniqueId +
                            " AND holdingSchemeName='" + product + "'";

                command.Parameters.AddWithValue("@TotalUnits", newTotalUnits);
                command.Parameters.AddWithValue("@Amount", newAmount);

                command.ExecuteNonQuery();

                // Update Transaction
                command.CommandText = @"insert into TransactionSummary values (@UniqueId, @TransactionDate, @Description, @Amount_Contributed, @TransactionType)";

                command.Parameters.AddWithValue("@UniqueId", uniqueId);
                command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                command.Parameters.AddWithValue("@Description", "Contrubuted units: " + units);
                command.Parameters.AddWithValue("@Amount_Contributed", contributedAmount);
                command.Parameters.AddWithValue("@TransactionType", "C");
                command.ExecuteNonQuery();
                connection.Close();
            }
            response.Status = "Success";
            return response;
        }

        public ValidationResponse ChangeSchemePreference(int uniqueId, int newSchemePreferenceId)
        {
            ValidationResponse response = new ValidationResponse();
            InfoService.IAccountInfoService infoService = new AccountInfoService();
            var schemes = infoService.GetCurrentSchemeDetails(uniqueId);

            if (!schemes.Any(x => x.SchemeId == newSchemePreferenceId))
            {
                response.Status = "Fail";
                response.ValidationMessage = "SchemeId not valid for user";
                return response;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"UPDATE SchemeInfo set IsPreferred=0
                                    WHERE IsPreferred=1 and uniqueId = " + uniqueId;
                    command.ExecuteNonQuery();

                    command.CommandText = @"UPDATE SchemeInfo set IsPreferred=1
                                    WHERE uniqueId = " + uniqueId +
                                " AND schemeId=" + newSchemePreferenceId;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                response.Status = "Fail";
                response.ValidationMessage = "Fail: DBException: " + e.StackTrace;
                return response;
            }
            response.Status = "Success";
            return response;
        }

        public ValidationResponse ChangeFundManagerName(int uniqueId, string fundManagerName)
        {
            ValidationResponse response = new ValidationResponse();
            IAccountInfoService infoService = new AccountInfoService();
            var account = infoService.ViewPersonalInfo(uniqueId);
            if (account == null)
            {
                response.Status = "Fail";
                response.ValidationMessage = "Fail: User not found";
                return response;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"UPDATE SchemeInfo set fundManagerName=@FundManagerName
                                    WHERE uniqueId = " + uniqueId;

                    command.Parameters.AddWithValue("@fundManagerName", fundManagerName);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                response.Status = "Fail";
                response.ValidationMessage = "Fail: DBException: " + e.StackTrace;
                return response;
            }
            response.Status = "Success";
            return response;
        }
    }

}
