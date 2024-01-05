using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Runtime.Serialization;

namespace DataContractLibrary
{
    [DataContract]
    public class FacilityResponse
    {
       

        private Client client;
        private string loanStatus;
        private decimal verifiedAmontBenchMark;
        private decimal clientSalaryAmount;
        private decimal loanAmountApproved;
        private string incomeTaxStatus;

        [DataMember]
        public Client CurrentClient
        {
            get { return client; }
            set { client = value; }
        }

         [DataMember]
        public String LoanStatus
        {
            get { return loanStatus; }
            set { loanStatus = value; }
        }

        [DataMember]
        public Decimal VerifiedAmontBenchMark
        {
            get { return verifiedAmontBenchMark; }
            set { verifiedAmontBenchMark = value; }
        }

        [DataMember]
        public Decimal ClientSalaryAmount
        {
            get { return clientSalaryAmount; }
            set { clientSalaryAmount = value; }
        }

        [DataMember]
        public Decimal LoanAmountApproved
        {
            get { return loanAmountApproved; }
            set { loanAmountApproved = value; }
        }

        [DataMember]
        public String IncomeTaxStatus
        {
            get { return incomeTaxStatus; }
            set { incomeTaxStatus = value; }
        }

        // Need default constructor to be serializable
        public FacilityResponse()
        {

        }

    }
}
