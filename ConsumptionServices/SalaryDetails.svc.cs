using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections;
using System.Web;
using Rules.ExternalRuleSetService;
using System.Workflow.Activities.Rules;
using Rules.ExternalRuleSetLibrary;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Activities;
using System.ServiceModel.Activation;

namespace ConsumptionServices
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SalaryDetails" in code, svc and config file together.
    public class SalaryDetails : ISalaryDetails
    {
        public decimal GetClientSalary(int clientId)
        {
            string tenantId = string.Empty;
            decimal salary = 0;

            if (HttpContext.Current.Session["TenantSession"] != null)
                tenantId = HttpContext.Current.Session["TenantSession"].ToString();

               ExecuteRulesSet execute = new ExecuteRulesSet();
               execute.RuleSetName = "ApplyClientRules";
               execute.MajorVersion = Convert.ToInt32(tenantId);
               execute.ValidationErrors = new ValidationErrorCollection();

              //The below section change be moved a translator file (DataCotract to Business entity translator)
               Rules.RulesLibrary.Client client = new Rules.RulesLibrary.Client();
               client.ClientId = clientId;
               execute.TargetObject = client;
               execute.ExecuteRules();
               salary = client.Salary;            

               return salary;
        }
    }
}
