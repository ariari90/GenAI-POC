using AggregatorSvcService.RequestHanders;
using Common;
using Common.Entities;
using DSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace AggregatorSvcService
{
    public class AggregatorSvc : IAggregatorSvc
    {
        private IAggregatorLog _log;

        public AggregatorResponse GetData(AggregatorRequest request)
        {
            InitLogger();
            
            AggregatorResponse response = null;
            RequestHandler handler = GetOrchestractorData(request);

            if (handler == null)
            {
                _log.LogError("Invalid Request Type");
                throw new Exception("Invalid Request Type");
            }

            response = handler.ProcessData();
            return response;
        }

        public RequestHandler GetOrchestractorData(AggregatorRequest request)
        {
            RequestHandler result = null;
            AggregatorConstants.Instantiate();

            if (request.RequestType == RequestType.AccountInfo)
            {
                _log.LogMessage("Initiating AccountInfo Handler");
                result = new InfoRequestHandler(request);
            }
            else if (request.RequestType == RequestType.HoldingsInfo)
            {
                _log.LogMessage("Initiating HoldingsRequestHandler Handler");
                result = new HoldingsRequestHandler(request);
            }
            else if (request.RequestType == RequestType.UpdateDetails)
            {
                _log.LogMessage("Initiating UpdateRequestHandler Handler");
                result = new UpdateRequestHandler(request);
            }
            else if (request.RequestType == RequestType.Transaction)
            {
                _log.LogMessage("Initiating TransactionRequestHandler Handler");
                result = new TransactionRequestHandler(request);
            }
            else if (request.RequestType == RequestType.AccountAndHoldings)
            {
                _log.LogMessage("Initiating AccountAndHoldingsRequestHandler Handler");
                result = new AccountAndHoldingsRequestHandler(request);
            }
            else if (request.RequestType == RequestType.Extra)
            {
                _log.LogMessage("Initiating ExtraRequestHandler Handler");
                result = new ExtraRequestHandler(request);
            }

            _log.LogMessage("Orchestration Process is completed");
            return result;
        }

        private void InitLogger()
        {
            _log = new AggregatorLog<AggregatorSvc>();
        }
    }
}
