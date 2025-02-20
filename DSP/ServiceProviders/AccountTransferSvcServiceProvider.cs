using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;

namespace DSP.ServiceProviders
{
    public class AccountTransferSvcServiceProvider : ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public AccountTransferSvcResponse AccountTransferSvcResponse { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            DSPLogger.LogMessage("Executing  AccountHoldingSvcResponse");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;
            AccountTransferSvcResponse accounttransferSvcResponse = null;

            try
            {
                if (Request != null && Request.AccountFundWorkflowRequest != null && Request.AccountFundWorkflowRequest.ExecuteWorkflow)
                {
                    AccountTransferService.AccountTransferServiceClient service = new AccountTransferService.AccountTransferServiceClient();
                    accounttransferSvcResponse = service.Execute(Request.AccountFundWorkflowRequest);
                }
            }
            catch (Exception e)
            {
                DSPLogger.LogError("Unexpected error occured: " + e.ToString());
                throw new Exception("Workflow error: " + e.ToString());
            }
            finally
            {
                if (accounttransferSvcResponse != null)
                {
                    SetDSFVariable(this, AggregatorConstants.AccountTransferSvcResponse, accounttransferSvcResponse);
                    SetDSFRequiredResponse(AggregatorConstants.AccountHoldingSvcResponse);
                    SetDSFRequiredResponse(AggregatorConstants.ExtraSvcResponse);

                }

            }
            return base.Execute(executionContext);
        }
    }
}
