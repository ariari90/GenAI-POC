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
using DataContractLibrary;

namespace MySequentialWorkflow
{
    public partial class PersonalInfo : SequenceActivity
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.InfoServiceResponse Response
        {
            get; set;
        }


        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        protected override void InitializeProperties()
        {
            base.InitializeProperties();
            
        }

        public static object GetValueOfWorkflowVariable(Activity activity, string valueName)
        {
            object value = null;
            if (activity != null)
            {
                try
                {
                    ActivityBind workflowActivityBind = new ActivityBind();
                    workflowActivityBind.Name = activity.Name;
                    workflowActivityBind.Path = valueName;
                    value = workflowActivityBind.GetRuntimeValue(activity);
                }
                catch
                { }
                if (value == null)
                    value = GetValueOfWorkflowVariable(activity.Parent, valueName);
            }
            return value;
        }

        public static void SetValueOfWorkflowVariable(Activity activity, string valueName, object value)
        {
            if (activity != null)
            {
                try
                {
                    ActivityBind workflowActivityBind = new ActivityBind();
                    workflowActivityBind.Name = activity.Name;
                    workflowActivityBind.Path = valueName;
                    workflowActivityBind.SetRuntimeValue(activity, value);
                }
                catch
                { }
                SetValueOfWorkflowVariable(activity.Parent, valueName, value);
            }
        }

        private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(Req.UniqueId);
            SetValueOfWorkflowVariable(this, "Request", Request);
            Console.WriteLine("Init test");
        }
    }
}
