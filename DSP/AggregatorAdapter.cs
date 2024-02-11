using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Workflow.ComponentModel;

namespace DSP
{
    public class AggregatorAdapter : ServiceProviderBase
    {
        public static DependencyProperty RequestProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Request", typeof(AggregatorRequest), typeof(AggregatorAdapter));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get
            {
                return ((AggregatorRequest)(base.GetValue(AggregatorAdapter.RequestProperty)));
            }

            set
            {
                base.SetValue(AggregatorAdapter.RequestProperty, value);
            }
        }

        public static DependencyProperty ResponseProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Response", typeof(AggregatorResponse), typeof(AggregatorAdapter));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorResponse Response
        {
            get
            {
                return ((AggregatorResponse)(base.GetValue(AggregatorAdapter.ResponseProperty)));
            }

            set
            {
                base.SetValue(AggregatorAdapter.ResponseProperty, value);
            }
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  Aggregator DSF Adapter");

            Response = new AggregatorResponse();

            Response = Aggregate<AggregatorResponse>(Response);
            SetDSFVariable(this.Parent, "Response", Response);

            Console.WriteLine("Response built");

            return base.Execute(executionContext);
        }

        public T Aggregate<T>(T aggregatorResponse, bool disableRequiredCheck = false, Type type = null)
        {
            if (type == null)
            {
                type = typeof(T);
            }
            
            T obj = (T)Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                var propType = prop.PropertyType;

                if (!disableRequiredCheck)
                {
                    if(!IsDSFResponseRequired(propType.Name))
                    {
                        // Do not instantiate property
                        continue;
                    }
                }

                bool isString = propType == typeof(string);
                bool isDateTime = propType == typeof(DateTime);
                bool isList = propType.Name.Contains("List");
                if (propType.IsClass && !isString && !isDateTime && !isList)
                {
                    string activityName = String.Empty;
                    
                    var instantiatePropObj = Activator.CreateInstance(propType);
                    var instantiateProp = Convert.ChangeType(instantiatePropObj, propType);

                    instantiatePropObj = Aggregate(instantiateProp, true, propType);
                    instantiateProp = Convert.ChangeType(instantiatePropObj, propType);
                    if (instantiateProp != null)
                    {
                        prop.SetValue(obj, instantiateProp, null);
                    }
                }
                else
                {
                    string activityName = String.Empty;

                    if (AggregatorConstants.keyServiceProvers.TryGetValue(prop.Name, out activityName))
                    {
                        object value = GetDSFVariableByName(activityName, prop.Name);
                        if (value != null)
                        {
                            prop.SetValue(obj, value, null);
                        }
                    }

                }
            }
            return obj;
        }
    }
}
