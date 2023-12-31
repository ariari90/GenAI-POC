using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ConsumptionServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISalaryDetails" in both code and config file together.
    [ServiceContract]
    public interface ISalaryDetails
    {
        [OperationContract]
        decimal GetClientSalary(int clientId);
    }
}
