using Common.Entities;
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
        AggregatorResponse _response;
        AutoResetEvent _waitHandle;

        public InfoRequestHandler(AggregatorRequest request)
        {
            _request = request;
            _response = new AggregatorResponse();
            _waitHandle = new AutoResetEvent(false);
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
                workflowRuntime.WorkflowCompleted += OnComplete;
                workflowRuntime.WorkflowTerminated += delegate (object sender, WorkflowTerminatedEventArgs e)
                {
                    Console.WriteLine(e.Exception.Message);
                    _waitHandle.Set();
                };

                Dictionary<string, object> inputs = new Dictionary<string, object>();
                inputs["Request"] = _request;
                WorkflowInstance instance = workflowRuntime.CreateWorkflow(typeof(Workflows.PersonalInfo), inputs);
                instance.Start();

                _waitHandle.WaitOne();

            }

            return _response;
        }

        private void OnComplete(object sender, WorkflowCompletedEventArgs e)
        {
            Console.WriteLine("OnComplete Fired....");

            _response = e.OutputParameters["Response"] as AggregatorResponse;
            Console.WriteLine("Response Received");
            _waitHandle.Set();
        }
    }
}