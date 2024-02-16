using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RequestService
{
    [ServiceContract]
    public interface IWithdrawService
    {
        [OperationContract]
        ValidationResponse WithdrawT1Amount(int uniqueId, string product, decimal withdrawPercent);

        [OperationContract]
        ValidationResponse ExitRequest(int uniqueId, string schemeName);

        [OperationContract]
        ExitRequestResponse GetExitStatus(int uniqueId, string schemeName);
    }
}
