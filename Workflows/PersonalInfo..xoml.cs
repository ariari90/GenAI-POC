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
using Common.Entities;
using System.Collections.Generic;
using DSP;

namespace Workflows
{
    public partial class PersonalInfo : SequenceActivity
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

        protected override void InitializeProperties()
        {
            base.InitializeProperties();
        }
    }
}
