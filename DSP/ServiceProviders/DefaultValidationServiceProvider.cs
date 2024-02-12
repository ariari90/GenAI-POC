using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;

namespace DSP
{
    public class DefaultValidationServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  UpdateSchemeServiceProvider");

            var validationResponse = GetValidationResponse();
            if (validationResponse == null)
            {
                validationResponse = new ValidationResponse();
                validationResponse.Status = "Success";
                SetValidationResponse(validationResponse);
            }

            return base.Execute(executionContext);
        }
    }
}
