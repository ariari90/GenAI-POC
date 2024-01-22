using DataContractLibrary;
using InfoService;
using RequestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrregatorSvc
{
    public class HoldingsRequestHandler: RequestHandler
    {
        AggregatorRequest _request;

        public HoldingsRequestHandler (AggregatorRequest request)
        {
            _request = request;
        }

        public override AggregatorResponse ProcessData()
        {
            AggregatorResponse response = new AggregatorResponse();
            response.HoldingsResponse = new HoldingsResponse();
            var bankingService = new AccountBankingService();
            response.HoldingsResponse.HoldingSummary = bankingService.GetHoldingSummary(_request.UniqueId);
            
            if (_request.HoldingsInfoRequest != null)
            {
                if (_request.HoldingsInfoRequest.ViewTransactionDateRange != null && _request.HoldingsInfoRequest.ViewTransactionDateRange.StartDate != null && _request.HoldingsInfoRequest.ViewTransactionDateRange.EndDate != null)
                {
                    response.HoldingsResponse.Transactions = bankingService.GetUserContribution(_request.UniqueId,
                        _request.HoldingsInfoRequest.ViewTransactionDateRange.StartDate.Value, _request.HoldingsInfoRequest.ViewTransactionDateRange.EndDate.Value);
                }

                if (!String.IsNullOrEmpty(_request.HoldingsInfoRequest.ViewExitRequestForSchemeName))
                {
                    IWithdrawService service = new WithdrawService();
                    response.HoldingsResponse.ExitRequestResponse = service.GetExitStatus(_request.UniqueId, _request.HoldingsInfoRequest.ViewExitRequestForSchemeName);
                }
            }

            return response;
        }
    }
}