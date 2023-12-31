using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.IO;
using System.Activities.XamlIntegration;
using System.Configuration;
using System.Reflection;
using DataContractLibrary;


namespace DecisionLibrary
{
    public sealed class VariedProcessDecision : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> AssemblyName { get; set; }

        // Define an activity input argument of type string
        public InArgument<string> NamespaceClassName { get; set; }

        // Define an activity input argument of type int
        public InArgument<object> Inputs { get; set; }


        public OutArgument<Dictionary<string, object>> result { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                // Obtain the runtime value of the Text input argument
                string assemblyName = context.GetValue(this.AssemblyName);
                string namespaceClassName = context.GetValue(this.NamespaceClassName);
                object input = context.GetValue(this.Inputs);


                Dictionary<string, object> inputs =  new Dictionary<string, object>() { { "workflowInput", input } };  
   
                // create stream with textbox contents
                StringBuilder data = new StringBuilder();

                Assembly assembly = Assembly.LoadFile(ConfigurationManager.AppSettings["XamlPath"].ToString() + assemblyName + ".dll");

                var dynamicActivity = assembly.CreateInstance(namespaceClassName) as Activity;

                IDictionary<string, object> results = WorkflowInvoker.Invoke(dynamicActivity, inputs);
                context.SetValue(result, results);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
