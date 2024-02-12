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
    public class SchemesServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public List<SchemeInfo> Schemes { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  SchemesServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                var schemes = service.GetCurrentSchemeDetails(Request.UniqueId);

                SetDSFVariable(this, AggregatorConstants.Schemes, schemes?.ToList());
                SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
            }

            return base.Execute(executionContext);
        }
    }
}
