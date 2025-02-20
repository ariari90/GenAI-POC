using Common.Entities;
using ExtraService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;

namespace DSP
{
    public class AccountHoldingSvcServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public AccountHoldingSvcResponse AccountHoldingSvcResponse { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  AccountHoldingSvcResponse");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;
            AccountHoldingSvcResponse accountHoldingSvcResponse = null;

            try
            {
                if (Request != null)
                {
                    AccountHoldingService service = new AccountHoldingService();
                    accountHoldingSvcResponse = service.Execute(Request.UniqueId);
                }
            }
            catch (Exception e)
            {
                DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                throw new Exception("Workflow error: " + e.ToString());
            }
            finally
            {
                if (accountHoldingSvcResponse != null)
                {
                    SetDSFVariable(this, AggregatorConstants.AccountHoldingSvcResponse, accountHoldingSvcResponse);
                    SetDSFRequiredResponse(AggregatorConstants.AccountHoldingSvcResponse);
                    SetDSFRequiredResponse(AggregatorConstants.ExtraSvcResponse);
                    
                }
                
            }
            return base.Execute(executionContext);
        }
    }
}
