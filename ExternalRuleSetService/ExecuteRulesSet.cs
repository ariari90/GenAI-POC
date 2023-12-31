using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.Activities.Rules;
using System.Activities;
using Rules.ExternalRuleSetLibrary;

namespace Rules.ExternalRuleSetService
{
    public class ExecuteRulesSet
    {
        /// <summary>
        /// RuleSetName
        /// </summary>
        public string RuleSetName { get; set; }

        /// <summary>
        /// MajorVersion
        /// </summary>
        public int MajorVersion { get; set; }

        /// <summary>
        /// MinorVersin
        /// </summary>
        public int MinorVersin { get; set; }

        /// <summary>
        /// TargetObject
        /// </summary>
        public object TargetObject { get; set; } 

        /// <summary>
        /// ValidationErrors
        /// </summary>
        public ValidationErrorCollection ValidationErrors { get; set; }

        /// <summary>
        /// ExecuteRules
        /// </summary>
        /// <returns></returns>
        public object ExecuteRules()
        {
            object evaluatedTarget = null;
            ExternalRuleSetService ruleSetService = new ExternalRuleSetService();
            RuleSet ruleSet = ruleSetService.GetRuleSet(new RuleSetInfo(RuleSetName, MajorVersion, 0));
            if (ruleSet != null)
            {
                Type targetType = TargetObject.GetType();
                RuleValidation validation = new RuleValidation(targetType, null);
                if (!ruleSet.Validate(validation))
                {
                    // Set the ValidationErrors OutArgument
                    ValidationErrors = validation.Errors;

                    // Throw exception
                    throw new ValidationException(string.Format("The ruleset is not valid. {0} validation errors found (check the ValidationErrors property for more information).", validation.Errors.Count));
                }

                // Execute the ruleset
                evaluatedTarget = TargetObject;
                RuleEngine engine = new RuleEngine(ruleSet, validation);
                engine.Execute(evaluatedTarget);
                
            }

            return evaluatedTarget;
        }
    }
}
