using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;

using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace MySequentialWorkflow
{
    public partial class InfoWorkflow : SequentialWorkflowActivity
    {
        private void codeActivity_ExecuteCode2(object sender, EventArgs e)
        {
            Console.WriteLine("Test");
        }
    }
}
