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
    public class PreferredSchemeServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public SchemeInfo PreferredScheme { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  MobileServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                SchemeInfo preferredScheme = null;
                try
                {
                    AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                    preferredScheme = service.GetSchemePreference(Request.UniqueId);
                }
                catch (Exception e)
                {
                    DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                    throw new Exception("Workflow error: " + e.ToString());
                }
                finally
                {
                    if (preferredScheme != null)
                    {
                        SetDSFVariable(this, AggregatorConstants.PreferredScheme, preferredScheme);
                        SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
                    }
                }
            }

            return base.Execute(executionContext);
        }
    }
}
