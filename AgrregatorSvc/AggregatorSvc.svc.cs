using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AgrregatorSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AggregatorSvc : IAggregatorSvc
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public FacilityResponse GetData(FacilityRequest request)
        {
            FacilityResponse response = new FacilityResponse();
            RequestHandler handler = GetOrchestractorData(request);

            return response;
        }

        public RequestHandler GetOrchestractorData(FacilityRequest request)
        {
            RequestHandler result = null;
            if(request.RequestType == RequestType.ActivityHistory)
            {
                result = new ActivityRequestHandler();
            }
            else if (request.RequestType == RequestType.AccountSummary)
            {
                result = new AccountSummaryRequestHandler();
            }
            else if(request.RequestType == RequestType.AccountList)
            {
                result = new AccountListRequestHandler();
            }
            return result;
        }
    }
}
