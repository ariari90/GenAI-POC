using Common;
using Common.Entities;
using InfoService;
using RequestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Workflow.Runtime;

namespace AggregatorSvcService
{
    public class UpdateRequestHandler : RequestHandler
    {
        AggregatorRequest _request;
        AggregatorResponse _response;
        AutoResetEvent _waitHandle;
        private readonly IAggregatorLog _log;

        public UpdateRequestHandler(AggregatorRequest request)
        {
            _request = request;
            _response = new AggregatorResponse();
            _waitHandle = new AutoResetEvent(false);
            _log = new AggregatorLog<TransactionRequestHandler>();
        }

        public override AggregatorResponse ProcessData()
        {            
            using (WorkflowRuntime workflowRuntime = new WorkflowRuntime())
            {
                workflowRuntime.WorkflowCompleted += OnComplete;
                workflowRuntime.WorkflowTerminated += OnTerminated;

                Dictionary<string, object> inputs = new Dictionary<string, object>();
                inputs["Request"] = _request;
                var orchestrator = AggregatorConstants.orchestratorConfig.Orchestrators.FirstOrDefault(x => x.Type == AggregatorConstants.DPO_UpdateRequest);
                if (orchestrator == null)
                {
                    _log.LogError("OrchestratorConfig does not contain the required key: " + AggregatorConstants.DPO_UpdateRequest);
                    throw new FaultException("Internal Server Error");
                }

                Assembly workflowAssembly = null;
                try
                {
                    workflowAssembly = Assembly.LoadFrom(orchestrator.Path);
                }
                catch (Exception e)
                {
                    _log.LogError("Could not load workflow assembly: " + e.ToString());
                    throw new FaultException("Internal Server Error");
                }

                var workflow = workflowAssembly.GetTypes().FirstOrDefault(x => x.Name == orchestrator.Type);
                WorkflowInstance instance = workflowRuntime.CreateWorkflow(workflow, inputs);
                instance.Start();

                _waitHandle.WaitOne();

            }

            return _response;
        }

        private void OnComplete(object sender, WorkflowCompletedEventArgs e)
        {
            _log.LogMessage("OnComplete Fired....");

            _response = e.OutputParameters["Response"] as AggregatorResponse;
            _log.LogMessage("Response Received");
            _waitHandle.Set();
        }

        private void OnTerminated(object sender, WorkflowTerminatedEventArgs e)
        {
            _log.LogError(e.Exception.Message);
            _waitHandle.Set();
            throw new FaultException("Unexpected Error in workflow orchestration");
        }
    }


}