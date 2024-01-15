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
       public AggregatorResponse GetData(AggregatorRequest request)
        {
            AggregatorResponse response = null;
            RequestHandler handler = GetOrchestractorData(request);
            
            if (handler == null)
            {
                throw new Exception("Invalid Request Type");
            }

            response = handler.ProcessData();
            return response;
        }

        public RequestHandler GetOrchestractorData(AggregatorRequest request)
        {
            RequestHandler result = null;
            if(request.RequestType == RequestType.AccountInfo)
            {
                result = new InfoRequestHandler(request);
            }
            else if (request.RequestType == RequestType.HoldingsInfo)
            {
                result = new HoldingsRequestHandler(request);
            }
            else if (request.RequestType == RequestType.UpdateDetails)
            {
                result = new UpdateRequestHandler(request);
            }
            else if (request.RequestType == RequestType.Transaction)
            {
                result = new TransactionRequestHandler(request);
            }
            else if (request.RequestType == RequestType.FacilityLicense)
            {
                result = new FacilityRequestHandler();
            }
            
            return result;
        }
    }
}
