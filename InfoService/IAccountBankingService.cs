using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InfoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAccountBankingService
    {
        [OperationContract]
        HoldingSummaryResponse GetHoldingSummary(int uid);

        [OperationContract]
        List<UserContributionData> GetUserContribution(int uid, DateTime startdate, DateTime enddate);

        [OperationContract]
        ValidationResponse UpdatePersonalDetails(PersonalDetails personDetails);
    }
}

