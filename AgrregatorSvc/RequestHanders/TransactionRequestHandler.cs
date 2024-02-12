using DataContractLibrary;
using InfoService;
using RequestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Workflow.Runtime;

namespace AgrregatorSvc
{
    public class TransactionRequestHandler : RequestHandler
    {
        AggregatorRequest _request;
        AggregatorResponse _response;
        AutoResetEvent _waitHandle;

        public TransactionRequestHandler(AggregatorRequest request)
        {
            _request = request;
            _response = new AggregatorResponse();
            _waitHandle = new AutoResetEvent(false);
        }

        public override AggregatorResponse ProcessData()
        {
            /*AggregatorResponse response = new AggregatorResponse();
            
            if (!ValidateRequest())
            {
                response.ValidationResponse = new ValidationResponse();
                response.ValidationResponse.Status = "Reject";
                response.ValidationResponse.ValidationMessage = "Contribution or Withdrawal inputs are not valid.";
                return response;
            }

            if (_request.ContributionRequest != null)
            {
                ContributionService.IContributionService service = new ContributionService.ContributionServiceClient();
                var validationResponse = service.ContributeOnline(_request.UniqueId, _request.ContributionRequest.Product, _request.ContributionRequest.Units);
                if (validationResponse.Status != "Success")
                {
                    response.ValidationResponse = validationResponse;
                    return response;
                }
            }

            if (_request.WithdrawRequest != null)
            {
                WithdrawService.WithdrawServiceClient service = new WithdrawService.WithdrawServiceClient();
                if (!_request.WithdrawRequest.IsExitRequest)
                {
                    var validationResponse = service.WithdrawT1Amount(_request.UniqueId, _request.WithdrawRequest.Product, _request.WithdrawRequest.WithdrawPercent);
                    if (validationResponse.Status != "Success")
                    {
                        response.ValidationResponse = validationResponse;
                        return response;
                    }
                }
                else
                {
                    var validationResponse = service.ExitRequest(_request.UniqueId, _request.WithdrawRequest.Product);
                    if (validationResponse.Status != "Success")
                    {
                        response.ValidationResponse = validationResponse;
                        return response;
                    }
                }
            }
            response.ValidationResponse = new ValidationResponse();
            response.ValidationResponse.Status = "Success";*/

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
                WorkflowInstance instance = workflowRuntime.CreateWorkflow(typeof(Workflows.Transaction), inputs);
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