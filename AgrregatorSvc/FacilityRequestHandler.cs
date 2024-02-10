using DataContractLibrary;
//using MulitenancyWcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrregatorSvc
{
    public class FacilityRequestHandler: RequestHandler
    {
        AggregatorRequest _request;

        public FacilityRequestHandler (AggregatorRequest request)
        {
            _request = request;
        }

        public object FacilityService { get; private set; }

        public override AggregatorResponse ProcessData()
        {
            AggregatorResponse response = new AggregatorResponse();
            
            if (!ValidateRequest())
            {
                response.ValidationResponse = new ValidationResponse();
                response.ValidationResponse.Status = "Reject";
                response.ValidationResponse.ValidationMessage = "Facility Request input is not valid.";
                return response;
            }

            //FacilityServiceClient.FacilityServiceClient client = new FacilityServiceClient.FacilityServiceClient();
            //response.FacilityResponse = client.CreateFacility(_request.FacilityRequest);
            return response;
        }

        public bool ValidateRequest()
        {
            if (_request.FacilityRequest == null)
            {
                return false;
            }
            return true;
        }
    }
}