using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InfoService
{
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