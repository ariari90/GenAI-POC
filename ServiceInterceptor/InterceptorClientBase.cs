using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ServiceInterceptor
{
    /// <summary>
    /// WCF Interceptor class for client base
    /// </summary>
    /// <typeparam name="T">The class to be intercepted</typeparam>
    public abstract class InterceptorClientBase<T> : ClientBase<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorClientBase&lt;T&gt;"/> class.
        /// </summary>
        public InterceptorClientBase()
        {
            Endpoint.Behaviors.Add(new MessageInspectorBehaviour(this));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorClientBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="endpointName">Name of the endpoint.</param>
        public InterceptorClientBase(string endpointName)
            : base(endpointName)
        {
            Endpoint.Behaviors.Add(new MessageInspectorBehaviour(this));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorClientBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="endpointName">Name of the endpoint.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public InterceptorClientBase(string endpointName, string remoteAddress)
            : base(endpointName, remoteAddress)
        {
            Endpoint.Behaviors.Add(new MessageInspectorBehaviour(this));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorClientBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="endpointName">Name of the endpoint.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public InterceptorClientBase(string endpointName, EndpointAddress remoteAddress)
            : base(endpointName, remoteAddress)
        {
            Endpoint.Behaviors.Add(new MessageInspectorBehaviour(this));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorClientBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public InterceptorClientBase(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {
            Endpoint.Behaviors.Add(new MessageInspectorBehaviour(this));
        }

        /// <summary>
        /// Handles the pre invocation event.
        /// </summary>
        /// <param name="request">The request.</param>
        internal virtual void PreInvoke(ref Message request)
        { }

        /// <summary>
        /// Handles the pre invocation event.
        /// </summary>
        /// <param name="reply">The reply.</param>
        internal virtual void PostInvoke(ref Message reply)
        { }


        /// <summary>
        /// End point behavior for message inspector
        /// </summary>
        public class MessageInspectorBehaviour : IEndpointBehavior, IClientMessageInspector
        {
            InterceptorClientBase<T> Proxy { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="InterceptorClientBase&lt;T&gt;.MessageInspectorBehaviour"/> class.
            /// </summary>
            /// <param name="proxy">The proxy.</param>
            internal MessageInspectorBehaviour(InterceptorClientBase<T> proxy)
            {
                Proxy = proxy;
            }


            /// <summary>
            /// Implements a modification or extension of the client across an endpoint.
            /// </summary>
            /// <param name="endpoint">The endpoint that is to be customized.</param>
            /// <param name="clientRuntime">The client runtime to be customized.</param>
            void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            {
                clientRuntime.MessageInspectors.Add(this);
            }

            /// <summary>
            /// Implement to pass data at runtime to bindings to support custom behavior.
            /// </summary>
            /// <param name="endpoint">The endpoint to modify.</param>
            /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
            void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
            { }

            /// <summary>
            /// Implements a modification or extension of the service across an endpoint.
            /// </summary>
            /// <param name="endpoint">The endpoint that exposes the contract.</param>
            /// <param name="endpointDispatcher">The endpoint dispatcher to be modified or extended.</param>
            void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
            { }

            /// <summary>
            /// Implement to confirm that the endpoint meets some intended criteria.
            /// </summary>
            /// <param name="endpoint">The endpoint to validate.</param>
            void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
            { }

            #region IClientMessageInspector Members

            /// <summary>
            /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
            /// </summary>
            /// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
            /// <param name="correlationState">Correlation state data.</param>
            void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
            {
                Proxy.PostInvoke(ref reply);
            }

            /// <summary>
            /// Enables inspection or modification of a message before a request message is sent to a service.
            /// </summary>
            /// <param name="request">The message to be sent to the service.</param>
            /// <param name="channel">The WCF client object channel.</param>
            /// <returns>
            /// The object that is returned as the <paramref name="correlationState "/>argument of the <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)"/> method. This is null if no correlation state is used.The best practice is to make this a <see cref="T:System.Guid"/> to ensure that no two <paramref name="correlationState"/> objects are the same.
            /// </returns>
            object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
            {
                Proxy.PreInvoke(ref request);
                return null;
            }

            #endregion
        }
    }
}
