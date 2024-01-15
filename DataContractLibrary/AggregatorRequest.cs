using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class AggregatorRequest
    {
        private int uniqueId;
        private RequestType requestType;
        private float amount;
        private DateRange transactionDateRange;
        private UpdateRequest updateRequest;
        private ContributionRequest contributionRequest;
        private WithdrawRequest withdrawRequest;
        private string viewExitRequestForSchemaName;

        [DataMember]
        public int UniqueId { get => uniqueId; set => uniqueId = value; }

        [DataMember]
        public float Amount { get => amount; set => amount = value; }

        [DataMember]
        public DateRange TransactionDateRange { get => transactionDateRange; set => transactionDateRange = value; }

        [DataMember]
        public ContributionRequest ContributionRequest { get => contributionRequest; set => contributionRequest = value; }

        [DataMember]
        public WithdrawRequest WithdrawRequest { get => withdrawRequest; set => withdrawRequest = value; }

        [DataMember]
        public string ViewExitRequestForSchemaName { get => viewExitRequestForSchemaName; set => viewExitRequestForSchemaName = value; }

        [DataMember]
        public RequestType RequestType { get => requestType; set => requestType = value; }

        [DataMember]
        public UpdateRequest UpdateRequest { get => updateRequest; set => updateRequest = value; }
    }
}
