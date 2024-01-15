using DataContractLibrary;
using InfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrregatorSvc
{
    public class InfoRequestHandler: RequestHandler
    {
        AggregatorRequest _request;
        public InfoRequestHandler(AggregatorRequest request)
        {
            _request = request;
        }

        public override AggregatorResponse ProcessData()
        {
            AggregatorResponse response = new AggregatorResponse();
            AccountInfoService accountInfoService = new AccountInfoService();
            response.AccountInfoResponse = new InfoServiceResponse();
            response.AccountInfoResponse.PersonalInfo = accountInfoService.ViewPersonalInfo(_request.UniqueId);
            response.AccountInfoResponse.BankInfo = accountInfoService.ViewBankInfo(_request.UniqueId);
            response.AccountInfoResponse.Schemas = accountInfoService.GetCurrentSchemeDetails(_request.UniqueId);
            response.AccountInfoResponse.PreferredScheme = accountInfoService.GetSchemePreference(_request.UniqueId);
            return response;
        }
    }
}