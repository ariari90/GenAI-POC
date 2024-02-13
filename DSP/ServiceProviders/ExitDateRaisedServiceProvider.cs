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
    public class ExitDateRaisedServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public Nullable<DateTime> DateRaised { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  ExitDateRaisedServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                if (!String.IsNullOrEmpty(Request.HoldingsInfoRequest.ViewExitRequestForSchemeName))
                {
                    WithdrawService.IWithdrawService service = new WithdrawService.WithdrawServiceClient();
                    var exitRequest = service.GetExitStatus(Request.UniqueId, Request.HoldingsInfoRequest.ViewExitRequestForSchemeName);
                    SetDSFVariable(this, AggregatorConstants.ExitDateRaised, exitRequest.DateRaised);
                    SetDSFRequiredResponse(AggregatorConstants.HoldingsResponse);
                }
                
            }

            return base.Execute(executionContext);
        }
    }
}
