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
    public partial class Transaction : SequenceActivity
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

        private void ContributeService_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = (Request.ContributionRequest != null);
        }

        private void WithdrawService_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = (Request.WithdrawRequest != null);
        }

        private void WithdrawServiceExecute_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = (Request.WithdrawRequest != null && !Request.WithdrawRequest.IsExitRequest);
        }

        private void ExitServiceExecute_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = (Request.WithdrawRequest != null && Request.WithdrawRequest.IsExitRequest);
        }
    }
}
