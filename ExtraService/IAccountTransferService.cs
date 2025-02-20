using Common;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ExtraService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountTransferService" in both code and config file together.
    [ServiceContract]
    public interface IAccountTransferService
    {
        [OperationContract]
        AccountTransferSvcResponse Execute(AccountTransferSvcRequest workflowRequest);

    }
}
