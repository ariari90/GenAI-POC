using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAuthWebsite
{
    class Program
    {
        static void Main(string[] args)
        {
            AggregatorSvcRef.AggregatorSvcClient client = new AggregatorSvcRef.AggregatorSvcClient();
            DataContractLibrary.SecuredService secured = new DataContractLibrary.SecuredService()
            {
                UserName = "admin",
                Password = "admin"
            };

            DataContractLibrary.AggregatorRequest request = new DataContractLibrary.AggregatorRequest();
            request.RequestType = DataContractLibrary.RequestType.AccountInfo;
            request.UniqueId = 1;
            var response = client.GetData(request);
            Console.WriteLine(response);
            Console.ReadKey();
        }
    }
}
