using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;

namespace DSP
{
    public class SchemesServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public List<SchemeInfo> Schemes { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  SchemesServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                SchemeInfo[] schemes = null;
                try
                {
                    AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                    schemes = service.GetCurrentSchemeDetails(Request.UniqueId);
                }
                catch (Exception e)
                {
                    DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                    throw new Exception("Workflow error: " + e.ToString());
                }
                finally
                {
                    if (schemes != null)
                    {
                        SetDSFVariable(this, AggregatorConstants.Schemes, schemes?.ToList());
                        SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
                    }
                }
            }

            return base.Execute(executionContext);
        }
    }
}
