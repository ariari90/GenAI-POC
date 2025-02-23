﻿using Common;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Workflow.Runtime;

namespace AggregatorSvcService
{
    public class AccountAndHoldingsRequestHandler: RequestHandler
    {
        AggregatorRequest _request;
        AggregatorResponse _response;
        AutoResetEvent _waitHandle;
        private readonly IAggregatorLog _log;

        public AccountAndHoldingsRequestHandler (AggregatorRequest request)
        {
            _response = new AggregatorResponse();
            _request = request;
            _waitHandle = new AutoResetEvent(false);
            _log = new AggregatorLog<AccountAndHoldingsRequestHandler>();
        }

        public object FacilityService { get; private set; }

        public override AggregatorResponse ProcessData()
        {
            _response.HoldingsResponse = new HoldingsResponse();

            using (WorkflowRuntime workflowRuntime = new WorkflowRuntime())
            {
                workflowRuntime.WorkflowCompleted += OnComplete;
                workflowRuntime.WorkflowTerminated += OnTerminated;

                Dictionary<string, object> inputs = new Dictionary<string, object>();
                inputs["Request"] = _request;
                WorkflowInstance instance = workflowRuntime.CreateWorkflow(typeof(Workflows.InfoRequest), inputs);
                instance.Start();

                _waitHandle.WaitOne();

                _log.LogMessage("Workflow complete");
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

        private void OnTerminated(object sender, WorkflowTerminatedEventArgs e)
        {
            _log.LogError(e.Exception.Message);
            _waitHandle.Set();
            throw new FaultException("Unexpected Error in workflow orchestration");
        }
    }
}