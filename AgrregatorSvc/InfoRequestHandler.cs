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
            response.InfoServiceResponse.PersonalInfo = accountInfoService.ViewPersonalInfo(_request.UniqueId);
            response.InfoServiceResponse.BankInfo = accountInfoService.ViewBankInfo(_request.UniqueId);
            response.InfoServiceResponse.Schemas = accountInfoService.GetCurrentSchemeDetails(_request.UniqueId);
            return response;
        }
    }
}