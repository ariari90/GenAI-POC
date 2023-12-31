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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IncomeTax" in code, svc and config file together.
    public class IncomeTax : IIncomeTax
    {
        public string VerifyTax(int clientId)
        {
           string result = "Income tax is verfied and not valid";

            //Database can be checked here at some centralized repository
           if (clientId > 4000)
               result = "Income Tax is Verfied and is valid.";

            return result;
        }
    }
}
