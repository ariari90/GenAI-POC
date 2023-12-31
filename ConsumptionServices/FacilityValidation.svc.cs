using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace ConsumptionServices
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FacilityValidation" in code, svc and config file together.
    public class FacilityValidation : IFacilityValidation
    { 
        public Decimal GetFacilityValidationAmountForFaciliytType(string FacilityType)
        {
            decimal amount = 0.00M;
            switch (FacilityType.ToLower())
            {
                case "facilitytype1":
                    amount = 10000.00M;
                    break;
                case "facilitytype2":
                    amount =20000.00M;
                    break;
                case "facilitytype3":
                    amount = 30000.00M;
                    break;
            }

            return amount;
        }
      
    }
}
