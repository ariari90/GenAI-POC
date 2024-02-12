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
    public class UpdateRequestHandler : RequestHandler
    {
        AggregatorRequest _request;
        AggregatorResponse _response;
        AutoResetEvent _waitHandle;

        public UpdateRequestHandler(AggregatorRequest request)
        {
            _request = request;
            _response = new AggregatorResponse();
            _waitHandle = new AutoResetEvent(false);
        }

        public override AggregatorResponse ProcessData()
        {            
            /*if (!ValidateRequest())
            {
                response.ValidationResponse = new ValidationResponse();
                response.ValidationResponse.Status = "Reject";
                response.ValidationResponse.ValidationMessage = "Inputs of update request are not provided correctly";
                return response;
            }

            if (_request.UpdateRequest.PersonalDetails != null)
            {
                AccountBankingService.IAccountBankingService bankingService = new AccountBankingService.AccountBankingServiceClient();
                var personalDetails = _request.UpdateRequest.PersonalDetails;
                personalDetails.Uniqueid = _request.UniqueId;
                var validationResponse = bankingService.UpdatePersonalDetails(_request.UpdateRequest.PersonalDetails);
                if (validationResponse.Status != "Success")
                {
                    response.ValidationResponse = validationResponse;
                    return response;
                }
            } 

            if (!String.IsNullOrEmpty(_request.UpdateRequest.FundManagerName))
            {
                IContributionService service = new ContributionService();
                var validationResponse = service.ChangeFundManagerName(_request.UniqueId, _request.UpdateRequest.FundManagerName);
                if (validationResponse.Status != "Success")
                {
                    response.ValidationResponse = validationResponse;
                    return response;
                }
            }

            if (_request.UpdateRequest.ChangeSchemeRequest != null)
            {
                IContributionService service = new ContributionService();
                var validationResponse = service.ChangeSchemePreference(_request.UniqueId, _request.UpdateRequest.ChangeSchemeRequest.NewSchemeId);
                if (validationResponse.Status != "Success")
                {
                    response.ValidationResponse = validationResponse;
                    return response;
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
                WorkflowInstance instance = workflowRuntime.CreateWorkflow(typeof(Workflows.UpdateRequest), inputs);
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