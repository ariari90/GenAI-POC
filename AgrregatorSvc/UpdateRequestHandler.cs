using DataContractLibrary;
using InfoService;
using RequestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrregatorSvc
{
    public class UpdateRequestHandler : RequestHandler
    {
        AggregatorRequest _request;

        public UpdateRequestHandler(AggregatorRequest request)
        {
            _request = request;
        }

        public override AggregatorResponse ProcessData()
        {
            AggregatorResponse response = new AggregatorResponse();
            
            if (!ValidateRequest())
            {
                response.ValidationResponse = new ValidationResponse();
                response.ValidationResponse.Status = "Reject";
                response.ValidationResponse.ValidationMessage = "Inputs of update request are not provided correctly";
                return response;
            }

            if (_request.UpdateRequest.PersonalDetails != null)
            {
                AccountBankingService.IAccountBankingService bankingService = new AccountBankingService.AccountBankingServiceClient();
                var personalDetails = _request.UpdateRequest.PersonalDetails;
                personalDetails.Uniqueid = _request.UniqueId;
                var validationResponse = bankingService.UpdatePersonalDetails(_request.UpdateRequest.PersonalDetails);
                if (validationResponse.Status != "Success")
                {
                    response.ValidationResponse = validationResponse;
                    return response;
                }
            } 

            if (!String.IsNullOrEmpty(_request.UpdateRequest.FundManagerName))
            {
                IContributionService service = new ContributionService();
                var validationResponse = service.ChangeFundManagerName(_request.UniqueId, _request.UpdateRequest.FundManagerName);
                if (validationResponse.Status != "Success")
                {
                    response.ValidationResponse = validationResponse;
                    return response;
                }
            }

            if (_request.UpdateRequest.ChangeSchemeRequest != null)
            {
                IContributionService service = new ContributionService();
                var validationResponse = service.ChangeSchemePreference(_request.UniqueId, _request.UpdateRequest.ChangeSchemeRequest.NewSchemeId);
                if (validationResponse.Status != "Success")
                {
                    response.ValidationResponse = validationResponse;
                    return response;
                }
            }

            response.ValidationResponse = new ValidationResponse();
            response.ValidationResponse.Status = "Success";
            return response;
        }

        public bool ValidateRequest()
        {
            if (_request.UpdateRequest == null)
            {
                return false;
            }

            if (_request.UpdateRequest.ChangeSchemeRequest == null && _request.UpdateRequest.PersonalDetails == null && String.IsNullOrEmpty(_request.UpdateRequest.FundManagerName))
            {
                return false;
            }

            return true;
        }
    }


}