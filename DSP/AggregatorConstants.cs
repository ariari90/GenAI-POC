using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

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
        public static string Schemes = "Schemes";

        public static Dictionary<string, string> keyServiceProvers = new Dictionary<string, string>();
        public static ServiceProviderConfig serviceProviderConfig = new ServiceProviderConfig();

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
            XmlSerializer deserializer = new XmlSerializer(typeof(ServiceProviderConfig));
            string configLocation = ConfigurationManager.AppSettings["ServiceProviderLocation"];
            string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), configLocation);
            
            TextReader textReader = new StreamReader(path);
            serviceProviderConfig = (ServiceProviderConfig)deserializer.Deserialize(textReader);
            textReader.Close();
        }
    }
}
