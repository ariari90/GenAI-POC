﻿using DataContractLibrary;
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
            
            if (_request.TransactionDateRange != null && _request.TransactionDateRange.StartDate!= null && _request.TransactionDateRange.EndDate != null)
            {
                response.HoldingsResponse.Transactions = bankingService.GetUserContribution(_request.UniqueId, 
                    _request.TransactionDateRange.StartDate.Value, _request.TransactionDateRange.EndDate.Value);
            }

            if (String.IsNullOrEmpty(_request.ViewExitRequestForSchemaName))
            {
                IWithdrawService service = new WithdrawService();
                response.HoldingsResponse.ExitRequestResponse = service.GetExitStatus(_request.UniqueId, _request.ViewExitRequestForSchemaName);
            }

            return response;
        }
    }
}