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
    public partial class UpdateRequest : SequenceActivity
    {
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Common.Entities.AggregatorResponse Response
        {
            get; set;
        }


        public Common.Entities.AggregatorRequest Request
        {
            get; set;
        }

        public ArrayList RequiredResponses;

        private void UpdatePersonalInfo_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = Request.UpdateRequest != null && Request.UpdateRequest.PersonalDetails != null;
        }

        private void UpdateFundManager_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = (Request.UpdateRequest != null && !String.IsNullOrEmpty(Request.UpdateRequest.FundManagerName));
        }

        private void UpdateSchemeService_IfActivity(object sender, ConditionalEventArgs e)
        {
            e.Result = (Request.UpdateRequest != null && Request.UpdateRequest.ChangeSchemeRequest != null);
        }
    }
}
