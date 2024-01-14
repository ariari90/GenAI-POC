using InfoService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RequestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class ContributionService : IContributionService
    {
        string _connectionString = @"Data Source=DESKTOP-2PDJ9M3; Database=gen_ai_poc; Initial Catalog=gen_ai_poc; Integrated Security=True";
        int _amountPerUnits = 10;

        public void ContributeOnline(int uniqueId, string product, int units)
        {
            if (units <= 0)
            {
                throw new Exception("You cannot contribute 0 units");
            }

            InfoService.IAccountBankingService bankingRequest = new InfoService.AccountBankingService();
            var holdingSummaryResponse = bankingRequest.GetHoldingSummary(uniqueId);
            var holdingForScheme = holdingSummaryResponse.HoldingSummaryData.Where(x => x.SchemeName == product).FirstOrDefault();

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
                            " AND schemeName='" + product + "'";

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
        }

        public string ChangeSchemePreference(int uniqueId, int newSchemePreferenceId)
        {
            InfoService.IAccountInfoService infoService = new AccountInfoService();
            var schemes = infoService.GetCurrentSchemeDetails(uniqueId);

            if (!schemes.Any(x => x.SchemeId == newSchemePreferenceId))
            {
                return ("Fail: SchemeId not valid for user.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"UPDATE SchemeInfo set IsPreferred=0
                                    WHERE IsPreferred=0=1 and uniqueId = " + uniqueId;
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
                return "Fail: DBException: " + e.StackTrace;
            }
            return "Success";
        }

        public string ChangeFundManagerName(int uniqueId, string fundManagerName)
        {
            IAccountInfoService infoService = new AccountInfoService();
            var account = infoService.ViewPersonalInfo(uniqueId);
            if (account == null)
            {
                return "Fail: User not found";
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"UPDATE AccountInfo set fundManagerName=@FundManagerName
                                    WHERE uniqueId = " + uniqueId;

                    command.Parameters.AddWithValue("@fundManagerName", fundManagerName);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                return "Fail: DBException: " + e.StackTrace;
            }
            return "Success";
        }
    }

}
