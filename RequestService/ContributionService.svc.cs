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

            using (var context = new DBEntities())
            {
                var holdingSummary = context.HoldingSummaries.FirstOrDefault(x => x.UniqueId == uniqueId && x.HoldingSchemeName == product);
                holdingSummary.TotalUnits = newTotalUnits;
                holdingSummary.Amount = newAmount;

                TransactionSummary transactionSummary = new TransactionSummary()
                {
                    UniqueId = uniqueId,
                    TransactionDate = DateTime.Now,
                    Description = "Contrubuted units: " + units,
                    Amount = contributedAmount,
                    TransactionType = "C"
                };

                context.TransactionSummaries.Add(transactionSummary);
                context.SaveChanges();

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
                using (var context = new DBEntities())
                {
                    if(context.SchemeInfoes.Any(x => x.IsPreferred == true && x.uniqueId == uniqueId))
                    {
                        var preferedSchemes = context.SchemeInfoes.Where(x => x.IsPreferred == true && x.uniqueId == uniqueId);
                        foreach (var scheme in preferedSchemes)
                        {
                            scheme.IsPreferred = false;
                        }
                    }

                    var currentPreferredScheme = context.SchemeInfoes.FirstOrDefault(x => x.uniqueId == uniqueId && x.schemeId == newSchemePreferenceId);
                    currentPreferredScheme.IsPreferred = true;
                    context.SaveChanges();
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
                using (var context = new DBEntities())
                {
                    var schemes = context.SchemeInfoes.Where(x => x.uniqueId == uniqueId);
                    foreach (var scheme in schemes)
                    {
                        scheme.fundManagerName = fundManagerName;
                    }
                    context.SaveChanges();
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
