using AggregatorSvcAuth;
using Common;
using Common.Entities;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TestAuthConsoleApp
{
    class Program
    {

        static void TestAggregator()
        {
            Console.WriteLine("Executing GetToken()");
            AuthInfo authInfo = new AuthInfo()
            { UserName = "admin", Password = "abcd" };
            var authUrl = new Uri("http://localhost:54413/AggregatorAuthService.svc/GetToken");
            WebRequest authWebRequest = WebRequest.Create(authUrl);
            string stringData = String.Empty;
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(typeof(AuthInfo));
                serializer.Serialize(stringwriter, authInfo);
                stringData = stringwriter.ToString();
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            var data = encoding.GetBytes(stringData);

            authWebRequest.Method = "POST";
            authWebRequest.ContentType = "application/x-www-form-urlencoded";
            authWebRequest.ContentLength = data.Length;

            Stream newStream = authWebRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            WebResponse authRespone = authWebRequest.GetResponse();

            StreamReader stream = new StreamReader(authRespone.GetResponseStream());
            string tokenResponse = stream.ReadToEnd();
             
            string token = XElement.Parse(tokenResponse).Value;
            Console.WriteLine("Token Receiced: " + token);
            Console.WriteLine("Executing GetData()");
            
            var uri = new Uri("http://localhost:54413/AggregatorAuthService.svc/GetData");
            
            WebRequest webRequest = WebRequest.Create(uri);
            AggregatorRequest request = new AggregatorRequest();
            request.RequestType = RequestType.AccountInfo;
            request.UniqueId = 1;

            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(typeof(AggregatorRequest));
                serializer.Serialize(stringwriter, request);
                stringData = stringwriter.ToString();
            }
            data = encoding.GetBytes(stringData);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = data.Length;

            webRequest.Headers.Add("Authorization", "Bearer " + token);

            newStream = webRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            WebResponse webResponse = webRequest.GetResponse();


            AggregatorResponse response = GetDataFromStream<AggregatorResponse>(webResponse.GetResponseStream());
            Console.ReadKey();
        }

        private static T GetDataFromStream<T>(Stream data)
        {
            StreamReader reader = new StreamReader(data);
            T response;
            string xmlString = reader.ReadToEnd();

            var doc = XDocument.Parse(xmlString);

            foreach (var element in doc.Descendants())
            {
                element.Attributes().Where(a => a.IsNamespaceDeclaration).Remove();
                element.Name = element.Name.LocalName;
            }
            xmlString = doc.ToString();

            using (var stringReader = new System.IO.StringReader(xmlString))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                response = (T)xmlSerializer.Deserialize(stringReader);
            }
            return response;
        }

        static void TestInfoService()
        {
            InfoService.AccountInfoService service = new InfoService.AccountInfoService();
            int uniqueId = 1;
            var personalInfo = service.ViewPersonalInfo(uniqueId);
            var bankInfo = service.ViewBankInfo(uniqueId);
            var schemePref = service.GetSchemePreference(uniqueId);
            var schemeDetails = service.GetCurrentSchemeDetails(uniqueId);
        }

        static void TestHoldings()
        {
            InfoService.IAccountBankingService service = new InfoService.AccountBankingService();
            int uniqueId = 1;
            PersonalDetails personalDetails = new PersonalDetails()
            {
                Address1 = "abc",
                Address2 = "xyz",
                City = "Kolkata",
                Mobile = "986129836",
                PinCode = 700034,
                Uniqueid = 1
            };

            var holdingSummary = service.GetHoldingSummary(uniqueId);
            var userContribution = service.GetUserContribution(uniqueId, DateTime.Now.AddDays(-1000), DateTime.Now);

            var validationResponse = service.UpdatePersonalDetails(personalDetails);
        }

        static void TestContribution()
        {
            RequestService.IContributionService service = new RequestService.ContributionService();
            int uniqueId = 1;
            string manager = "XYZ";
            service.ChangeFundManagerName(uniqueId, manager);
            service.ChangeSchemePreference(uniqueId, 1);
            service.ContributeOnline(uniqueId, "scheme1", 20);
        }


        static void Main(string[] args)
        {
            TestAggregator();
            TestInfoService();
            TestHoldings();
            TestContribution();
        }
    }
}
