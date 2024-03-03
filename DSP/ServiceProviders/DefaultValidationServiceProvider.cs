using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using Common;

namespace DSP
{
    public class DefaultValidationServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  UpdateSchemeServiceProvider");

            ValidationResponse validationResponse = new ValidationResponse();
            try
            {
                validationResponse = GetValidationResponse();
            }
            catch (Exception e)
            {
                DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                throw new Exception("Workflow error: " + e.ToString());
            }
            finally
            {
                if (validationResponse == null)
                {
                    validationResponse = new ValidationResponse();
                    validationResponse.Status = "Success";
                    SetValidationResponse(validationResponse);
                }
            }

            return base.Execute(executionContext);
        }
    }
}
