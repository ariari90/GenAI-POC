using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RequestService
{
    [ServiceContract]
    public interface IContributionService
    {
        [OperationContract]
        ValidationResponse ContributeOnline(int uniqueId, string product, int units);

        [OperationContract]
        ValidationResponse ChangeSchemePreference(int uniqueId, int newSchemePreferenceId);

        [OperationContract]
        ValidationResponse ChangeFundManagerName(int uniqueId, string fundManagerName);
    }
}
