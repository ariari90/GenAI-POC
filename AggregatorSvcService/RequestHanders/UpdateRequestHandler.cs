﻿using Common;
using Common.Entities;
using InfoService;
using RequestService;
using System;
using System.Collections.Generic;
using System.Linq;
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
                WorkflowInstance instance = workflowRuntime.CreateWorkflow(typeof(Workflows.UpdateRequest), inputs);
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