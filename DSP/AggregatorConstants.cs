using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSP
{
    public class AggregatorConstants
    {
        public static string InfoServiceResponse = "InfoServiceResponse";
        public static string HoldingsResponse = "HoldingsResponse";
        public static string ValidationResponse = "ValidationResponse";


        public static string RequiredResponses = "RequiredResponses";

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

        public static Dictionary<string, string> keyServiceProvers = new Dictionary<string, string>();

        public static void Instantiate()
        {
            SetupKeyServiceProviders();
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
        }
    }
}
