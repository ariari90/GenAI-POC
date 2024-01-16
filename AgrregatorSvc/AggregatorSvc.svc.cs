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
