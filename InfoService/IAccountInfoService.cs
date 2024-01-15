using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace InfoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAccountInfoService
    {
        [OperationContract]
        PersonalInfo ViewPersonalInfo(int uniqueId);

        [OperationContract]
        BankInfo ViewBankInfo(int uniqueId);

        [OperationContract]
        List<SchemeInfo> GetCurrentSchemeDetails(int uniqueId);

        [OperationContract]
        SchemeInfo GetSchemePreference(int uniqueId);
    }

}
