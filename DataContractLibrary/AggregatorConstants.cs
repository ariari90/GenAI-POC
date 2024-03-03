using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Common
{
    public class AggregatorConstants
    {
        private static bool _isInitialized = false;
        private static string ServiceProviderConfigLocation = "ServiceProviderConfigLocation";
        private static string OrchrestratorConfigLocation = "OrchestratorConfigLocation";

        public static string DPO_InfoRequest = "InfoRequest";
        public static string DPO_PersonalInfo = "PersonalInfo";
        public static string DPO_Holdings = "Holdings";
        public static string DPO_Transaction = "Transaction";
        public static string DPO_UpdateRequest = "UpdateRequest";

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

        public static ServiceProviderConfig serviceProviderConfig = new ServiceProviderConfig();
        public static OrchestratorConfig orchestratorConfig = new OrchestratorConfig();

        public static void Instantiate()
        {
            if (!_isInitialized)
            {
                SetupKeyServiceProviders();
                SetupOrchestratorPaths();
                _isInitialized = true;
            }
        }
        
        private static void SetupKeyServiceProviders()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ServiceProviderConfig));
            string configLocation = ConfigurationManager.AppSettings[ServiceProviderConfigLocation];
            string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), configLocation);
            
            TextReader textReader = new StreamReader(path);
            serviceProviderConfig = (ServiceProviderConfig)deserializer.Deserialize(textReader);
            textReader.Close();
        }

        private static void SetupOrchestratorPaths()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(OrchestratorConfig));
            string configLocation = ConfigurationManager.AppSettings[OrchrestratorConfigLocation];
            string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), configLocation);

            TextReader textReader = new StreamReader(path);
            orchestratorConfig = (OrchestratorConfig)deserializer.Deserialize(textReader);
            textReader.Close();
        }
    }
}
