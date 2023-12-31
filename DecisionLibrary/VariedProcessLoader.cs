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
    public class VariedProcessLoader
    {
        public IDictionary<string, object> OutputResult { get; set; }

        public string AssemblyNameInvoke { get; set; }

        public string NamespaceClassNameInvoke { get; set; }

        // Define an activity input argument of type int
        public object InputRequest { get; set; }

        public void ExecuteVariedProcessLoad()
        {
            try
            {
                Dictionary<string, object> inputs = new Dictionary<string, object>() { { "workflowInput", InputRequest } };

                // create stream with textbox contents
                StringBuilder data = new StringBuilder();

                Assembly assembly = Assembly.LoadFile(ConfigurationManager.AppSettings["XamlPath"].ToString() + AssemblyNameInvoke + ".dll");

                var dynamicActivity = assembly.CreateInstance(NamespaceClassNameInvoke) as Activity;

                OutputResult = WorkflowInvoker.Invoke(dynamicActivity, inputs);

            }
            catch (Exception ex)
            {
            }
        }

    }
}
