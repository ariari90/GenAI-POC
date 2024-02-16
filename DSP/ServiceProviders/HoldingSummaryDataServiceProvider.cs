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
    public class HoldingSummaryDataServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public List<HoldingSummaryData> HoldingSummaryData { get; set; } 

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  HoldingSummaryDataServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                HoldingSummaryResponse holdings = null;
                try
                {
                    AccountBankingService.AccountBankingServiceClient service = new AccountBankingService.AccountBankingServiceClient();
                    holdings = service.GetHoldingSummary(Request.UniqueId);
                }
                catch (Exception e)
                {
                    DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                    throw new Exception("Workflow error: " + e.ToString());
                }
                finally
                {
                    SetDSFVariable(this, AggregatorConstants.HoldingSummaryData, holdings.HoldingSummaryData);
                    SetDSFRequiredResponse(AggregatorConstants.HoldingsResponse);
                }
            }

            return base.Execute(executionContext);
        }
    }
}
