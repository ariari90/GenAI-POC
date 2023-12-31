using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rules.RulesLibrary
{
    public class Client
    {
        private int clientId;
        private string clientName;
        private decimal facilityAmount;
        private string facilityStatus;
        private string facilityStatusRuleVerified;
        private decimal salary;
       
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

     
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        public string FacilityStatus
        {
            get { return facilityStatus; }
            set { facilityStatus = value; }
        }

        public Decimal FacilityAmount
        {
            get { return facilityAmount; }
            set { facilityAmount = value; }
        }

        public string FacilityStatusRuleVerified
        {
            get { return facilityStatusRuleVerified; }
            set { facilityStatusRuleVerified = value; }
        }

        public Decimal Salary
        {
            get { return salary; }
            set { salary = value; }
        }
       
    }
}
