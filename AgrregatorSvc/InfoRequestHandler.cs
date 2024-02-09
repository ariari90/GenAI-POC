using DataContractLibrary;
using InfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Workflow.Runtime;

namespace AgrregatorSvc
{
    public class InfoRequestHandler: RequestHandler
    {
        AggregatorRequest _request;
        AggregatorResponse response = new AggregatorResponse();

        public InfoRequestHandler(AggregatorRequest request)
        {
            _request = request;
        }

        public override AggregatorResponse ProcessData()
        {
            
            /*AccountInfoService accountInfoService = new AccountInfoService();
            response.AccountInfoResponse = new InfoServiceResponse();
            response.AccountInfoResponse.PersonalInfo = accountInfoService.ViewPersonalInfo(_request.UniqueId);
            response.AccountInfoResponse.BankInfo = accountInfoService.ViewBankInfo(_request.UniqueId);
            response.AccountInfoResponse.Schemes = accountInfoService.GetCurrentSchemeDetails(_request.UniqueId);
            response.AccountInfoResponse.PreferredScheme = accountInfoService.GetSchemePreference(_request.UniqueId);*/
            using (WorkflowRuntime workflowRuntime = new WorkflowRuntime())
            {
                AutoResetEvent waitHandle = new AutoResetEvent(false);
                workflowRuntime.WorkflowCompleted += OnComplete;
                workflowRuntime.WorkflowTerminated += delegate (object sender, WorkflowTerminatedEventArgs e)
                {
                    Console.WriteLine(e.Exception.Message);
                    waitHandle.Set();
                };

                Dictionary<string, object> inputs = new Dictionary<string, object>();
                inputs["Request"] = _request;
                WorkflowInstance instance = workflowRuntime.CreateWorkflow(typeof(MySequentialWorkflow.PersonalInfo), inputs);
                instance.Start();

                waitHandle.WaitOne();

                Console.ReadKey();
            }

            return response;
        }

        private void OnComplete(object sender, WorkflowCompletedEventArgs e)
        {
            Console.WriteLine("OnComplete Fired....");

            response = e.OutputParameters["Response"] as AggregatorResponse;
            Console.WriteLine("Response Received");
        }
    }
}