#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;

#endregion

namespace MySequentialWorkflow
{
    class Program
    {
        static void Main(string[] args)
        {
            using(WorkflowRuntime workflowRuntime = new WorkflowRuntime())
            {
                AutoResetEvent waitHandle = new AutoResetEvent(false);
                workflowRuntime.WorkflowCompleted += OnComplete;
                workflowRuntime.WorkflowTerminated += delegate(object sender, WorkflowTerminatedEventArgs e)
                {
                    Console.WriteLine(e.Exception.Message);
                    waitHandle.Set();
                };

                Dictionary<string, object> inputs = new Dictionary<string, object>();
                inputs["SetX"] = 2;
                WorkflowInstance instance = workflowRuntime.CreateWorkflow(typeof(MySequentialWorkflow.PersonalInfo), inputs);
                instance.Start();

                waitHandle.WaitOne();

                Console.ReadKey();
            }
        }

        private static void OnComplete(object sender, WorkflowCompletedEventArgs e)
        {
            Console.WriteLine("OnComplete Fired....");
            int result = (int) e.OutputParameters["Result"];
            Console.WriteLine("Result is: " + result);
        }
    }
}
