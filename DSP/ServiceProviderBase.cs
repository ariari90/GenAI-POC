using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using DataContractLibrary;
using System.Collections;

namespace DSP
{
    public class ServiceProviderBase : System.Workflow.ComponentModel.Activity
    {

        public void SetDSFRequiredResponse(string responseName)
        {
            ArrayList requiredResponses = GetDSFVariable(this, AggregatorConstants.RequiredResponses) as ArrayList;
            if (requiredResponses == null)
            {
                requiredResponses = new ArrayList();
            }

            requiredResponses.Add(responseName);

            SetDSFVariable(this, AggregatorConstants.RequiredResponses, requiredResponses);
        }

        public bool IsDSFResponseRequired(string responseName)
        {
            ArrayList requiredResponses = GetDSFVariable(this, AggregatorConstants.RequiredResponses) as ArrayList;
            if (requiredResponses == null)
            {
                return false;
            }
            return requiredResponses.Contains(responseName);
        }

        public object GetDSFVariableByName (string activityName, string valueName)
        {
            var activity = GetActivityByName(activityName);
            if(activity == null)
            {
                return null;
            }

            return GetDSFVariable(activity, valueName);
        }

        public object GetDSFVariable(Activity activity, string valueName)
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
                    value = GetDSFVariable(activity.Parent, valueName);
            }
            return value;
        }

        
        public static void SetDSFVariable(Activity activity, string valueName, object value)
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
                SetDSFVariable(activity.Parent, valueName, value);
            }
        }
    }
}
