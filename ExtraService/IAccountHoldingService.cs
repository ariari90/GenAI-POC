

using Common.Entities;
using System.ServiceModel;

namespace ExtraService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountHoldingService" in both code and config file together.
    [ServiceContract]
    public interface IAccountHoldingService
    {
        [OperationContract]
        AccountHoldingSvcResponse Execute(int uniqueId);
    }
}
