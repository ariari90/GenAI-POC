using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using Common.Entities;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Workflow.ComponentModel.Design;
using System.Drawing.Drawing2D;
using Common;

namespace DSP
{

    public class ServiceProviderBase : System.Workflow.ComponentModel.Activity
    {
        public IAggregatorLog DSPLogger { get; set; }

        protected override void Initialize(IServiceProvider provider)
        {
            InstantiateLog();
        }

        private void InstantiateLog()
        {
            Type superType = this.GetType();
            Type genericLoggerType = typeof(AggregatorLog<>).MakeGenericType(superType);
            DSPLogger = (IAggregatorLog)Activator.CreateInstance(genericLoggerType);
        }

        public void SetValidationResponse(ValidationResponse validationResponse)
        {
            AggregatorResponse response = GetDSFVariable(this.Parent, AggregatorConstants.Response) as AggregatorResponse;
            response = new AggregatorResponse();
            response.ValidationResponse = validationResponse;
            SetDSFVariable(this.Parent, AggregatorConstants.Response, response);
        }

        public ValidationResponse GetValidationResponse()
        {
            AggregatorResponse response = GetDSFVariable(this.Parent, AggregatorConstants.Response) as AggregatorResponse;
            return response?.ValidationResponse;
        }

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
