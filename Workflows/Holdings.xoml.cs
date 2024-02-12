using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Workflows
{
    public partial class Holdings : SequenceActivity
    {
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorResponse Response
        {
            get; set;
        }


        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public ArrayList RequiredResponses;

        private void Transactions_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = (Request != null && Request.HoldingsInfoRequest != null && Request.HoldingsInfoRequest.ViewTransactionDateRange != null && Request.HoldingsInfoRequest.ViewTransactionDateRange.StartDate != null && Request.HoldingsInfoRequest.ViewTransactionDateRange.EndDate != null);
        }

        private void ExitRequest_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = Request.HoldingsInfoRequest != null && !String.IsNullOrEmpty(Request.HoldingsInfoRequest.ViewExitRequestForSchemeName);
        }
    }
}
