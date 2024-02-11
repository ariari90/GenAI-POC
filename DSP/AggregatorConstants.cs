using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSP
{
    public class AggregatorConstants
    {
        private static bool _isInitialized = false;

        public static string InfoServiceResponse = "InfoServiceResponse";
        public static string HoldingsResponse = "HoldingsResponse";
        public static string ValidationResponse = "ValidationResponse";


        public static string RequiredResponses = "RequiredResponses";

        public static string Response = "Response";
        public static string Mobile = "Mobile";
        public static string Address1 = "Address1";
        public static string Address2 = "Address2";
        public static string City = "City";
        public static string PinCode = "PinCode";
        public static string FullName = "FullName";
        public static string FathersName = "FathersName";
        public static string MothersName = "MothersName";
        public static string Nationality = "Nationality";
        public static string DateOfBirth = "DateOfBirth";
        public static string IsKycDone = "IsKycDone";
        public static string AccountNumber = "AccountNumber";
        public static string BankBranch = "BankBranch";
        public static string BankName = "BankName";
        public static string Address = "Address";
        public static string IfscCode = "IfscCode";
        public static string SchemeInfo = "SchemeInfo";
        public static string PreferredScheme = "PreferredScheme";
        public static string HoldingSummaryData = "HoldingSummaryData";
        public static string TotalAmount = "TotalAmount";
        public static string Transactions = "Transactions";
        public static string ExitDateRaised = "DateRaised";
        public static string ExitDateStatus = "Status";
        public static string UniqueId = "UniqueId";

        public static Dictionary<string, string> keyServiceProvers = new Dictionary<string, string>();

        public static void Instantiate()
        {
            if (!_isInitialized)
            {
                SetupKeyServiceProviders();
                _isInitialized = true;
            }
        }
        
        public static void SetupKeyServiceProviders()
        {
            keyServiceProvers.Add(Mobile, "mobileServiceProvider1");
            keyServiceProvers.Add(Address1, "addressServiceProvider1");
            keyServiceProvers.Add(Address2, "addressServiceProvider1");
            keyServiceProvers.Add(City, "addressServiceProvider1");
            keyServiceProvers.Add(PinCode, "addressServiceProvider1");
            keyServiceProvers.Add(FullName, "basicUserInfoServiceProvider1");
            keyServiceProvers.Add(FathersName, "basicUserInfoServiceProvider1");
            keyServiceProvers.Add(MothersName, "basicUserInfoServiceProvider1");
            keyServiceProvers.Add(Nationality, "basicUserInfoServiceProvider1");
            keyServiceProvers.Add(DateOfBirth, "basicUserInfoServiceProvider1");
            keyServiceProvers.Add(IsKycDone, "kycServiceProvider1");
            keyServiceProvers.Add(AccountNumber, "accountNumberServiceProvider1");
            keyServiceProvers.Add(BankBranch, "bankBranchServiceProvider1");
            keyServiceProvers.Add(BankName, "bankNameServiceProvider1");
            keyServiceProvers.Add(Address, "bankAddressServiceProvider1");
            keyServiceProvers.Add(IfscCode, "ifscCodeServiceProvider1");
            keyServiceProvers.Add(SchemeInfo, "schemeNameServiceProvider1");
            keyServiceProvers.Add(PreferredScheme, "preferredSchemeServiceProvider1");
            keyServiceProvers.Add(HoldingSummaryData, "holdingSummaryDataServiceProvider1");
            keyServiceProvers.Add(TotalAmount, "holdingsTotalAmountServiceProvider1");
            keyServiceProvers.Add(Transactions, "transactionsServiceProvider1");
            keyServiceProvers.Add(ExitDateRaised, "exitDateRaisedServiceProvider1");
            keyServiceProvers.Add(ExitDateStatus, "exitDateStatusServiceProvider1");
            keyServiceProvers.Add(UniqueId, "uniqueIdServiceProvider1");
        }
    }
}
