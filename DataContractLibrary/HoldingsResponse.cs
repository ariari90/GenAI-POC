using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class HoldingsResponse
    {
        private HoldingSummaryResponse holdingSummary;
        private List<UserContributionData> transactions;
        private ExitRequestResponse exitRequestResponse;

        [DataMember]
        public HoldingSummaryResponse HoldingSummary { get => holdingSummary; set => holdingSummary = value; }

        [DataMember]
        public List<UserContributionData> Transactions { get => transactions; set => transactions = value; }

        [DataMember]
        public ExitRequestResponse ExitRequestResponse { get => exitRequestResponse; set => exitRequestResponse = value; }
    }
}
