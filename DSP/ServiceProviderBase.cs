using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;

namespace DSP
{
    public class ServiceProviderBase : System.Workflow.ComponentModel.Activity
    {
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
    }
}
