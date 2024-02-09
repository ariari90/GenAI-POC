using DataContractLibrary;
using InfoService;
using RequestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrregatorSvc
{
    public class TransactionRequestHandler : RequestHandler
    {
        AggregatorRequest _request;
        public TransactionRequestHandler(AggregatorRequest request)
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
                response.ValidationResponse.ValidationMessage = "Contribution or Withdrawal inputs are not valid.";
                return response;
            }

            if (_request.ContributionRequest != null)
            {
                IContributionService service = new ContributionService();
                var validationResponse = service.ContributeOnline(_request.UniqueId, _request.ContributionRequest.Product, _request.ContributionRequest.Units);
                if (validationResponse.Status != "Success")
                {
                    response.ValidationResponse = validationResponse;
                    return response;
                }
            }

            if (_request.WithdrawRequest != null)
            {
                IWithdrawService service = new WithdrawService();
                if (!_request.WithdrawRequest.IsExitRequest)
                {
                    var validationResponse = service.WithdrawT1Amount(_request.UniqueId, _request.WithdrawRequest.Product, _request.WithdrawRequest.WithdrawPercent);
                    if (validationResponse.Status != "Success")
                    {
                        response.ValidationResponse = validationResponse;
                        return response;
                    }
                }
                else
                {
                    var validationResponse = service.ExitRequest(_request.UniqueId, _request.WithdrawRequest.Product);
                    if (validationResponse.Status != "Success")
                    {
                        response.ValidationResponse = validationResponse;
                        return response;
                    }
                }
            }
            response.ValidationResponse = new ValidationResponse();
            response.ValidationResponse.Status = "Success";
            return response;
        }

        private bool ValidateRequest()
        {
            if (_request.ContributionRequest == null && _request.WithdrawRequest == null)
            {
                return false;
            }

            if(_request.ContributionRequest != null && String.IsNullOrEmpty(_request.ContributionRequest.Product))
            {
                return false;
            }
            return true;
        }
    }
}