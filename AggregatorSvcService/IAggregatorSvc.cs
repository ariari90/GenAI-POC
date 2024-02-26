using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AggregatorSvcService
{
    [ServiceContract]
    public interface IAggregatorSvc
    {
        [OperationContract]
        AggregatorResponse GetData(AggregatorRequest request);        
    }
}
