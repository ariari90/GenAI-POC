using DataContractLibrary;
using DSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace AgrregatorSvc
{
    public class AggregatorSvc : IAggregatorSvc
    {
        public SecuredService SoapHeader;

        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string AuthenticationMethod()
        {
            if(SoapHeader == null)
            {
                return String.Empty;
            }

            if (String.IsNullOrEmpty(SoapHeader.UserName) || String.IsNullOrEmpty(SoapHeader.Password))
            {
                return String.Empty;
            }

            if (!IsUserCredentialsValid(SoapHeader.UserName, SoapHeader.Password))
            {
                return String.Empty;
            }

            string token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(
                token,
                SoapHeader.UserName,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                TimeSpan.FromMinutes(30),
                System.Web.Caching.CacheItemPriority.NotRemovable,
                null);

            return token;
        }

        public bool IsUserCredentialsValid(string userName, string password)
        {
            if (userName == "admin" && password == "admin")
            {
                return true;
            }
            return false;
        }

        public bool IsUserCredentialsValid(SecuredService SoapHeader)
        {
            if (SoapHeader == null)
            {
                return false;
            }

            if (!String.IsNullOrEmpty(SoapHeader.AuthenticationToken))
            {
                return (System.Web.HttpRuntime.Cache[SoapHeader.AuthenticationToken] != null);

            }
            return false;
        }

        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public AggregatorResponse GetData(AggregatorRequest request)
        {
            /*if (SoapHeader == null)
            {
                return null;
            }

            if(!IsUserCredentialsValid(SoapHeader))
            {
                return null;
            }*/

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
            AggregatorConstants.Instantiate();

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
            else if (request.RequestType == RequestType.AccountAndHoldings)
            {
                result = new AccountAndHoldingsRequestHandler(request);
            }
            
            return result;
        }
    }
}
