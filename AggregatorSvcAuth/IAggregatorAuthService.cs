using Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.UI;

namespace AggregatorSvcAuth
{
    [ServiceContract]
    public interface IAggregatorAuthService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetToken")]
        string GetToken(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetData")]
        AggregatorResponse GetData(Stream data);
    }

    
}
