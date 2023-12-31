using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Text;
using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace ServiceInterceptor
{
    /// <summary>
    /// The custom wcf endpoint behaviour to allow policy injection
    /// </summary>
    public class InjectionEndpointBehavior : BehaviorExtensionElement, IEndpointBehavior, IDispatchMessageInspector
    {
        #region Base class methods override

        /// <summary>
        /// Read only property to obtain the type of behaviour
        /// </summary>
        public override Type BehaviorType
        {

            get { return typeof(InjectionEndpointBehavior); }
        }

        /// <summary>
        ///  Creates a behavior extension based on the current configuration settings.
        ///  Overridden from base to return the custom behaviour
        /// </summary>
        /// <returns>Tyhe custom behaviour object.</returns>
        protected override object CreateBehavior()
        {
            return new InjectionEndpointBehavior();
        }

        #endregion

        #region IEndpointBehavior Members

        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="endpoint"> The endpoint to modify.</param>
        /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // No implementation required
            return;
        }

        /// <summary>
        /// Implements a modification or extension of the client across an endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint that is to be customized.</param>
        /// <param name="clientRuntime">The client runtime to be customized.</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // No implementation required

            return;
        }

        /// <summary>
        /// Implements a modification or extension of the service across an endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint that exposes the contract.</param>
        /// <param name="endpointDispatcher">The endpoint dispatcher to be modified or extended.</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            Type contractType = endpoint.Contract.ContractType;
            endpointDispatcher.DispatchRuntime.InstanceProvider = new InjectionInstanceProvider(contractType);
           //IErrorHandler errorHandler = new ExtendedServiceErrorHandler();

            ChannelDispatcher channelDispatcher = endpointDispatcher.ChannelDispatcher;
            if (channelDispatcher != null)
            {
                foreach (EndpointDispatcher ed in channelDispatcher.Endpoints)
                {
                    ed.DispatchRuntime.MessageInspectors.Add(new InjectionEndpointBehavior());
                }
                //add instance of IErrorHandler which is used for error handling and 
                //shielding at service boundaries
               // channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }

        /// <summary>
        /// Implement to confirm that the endpoint meets some intended criteria.
        /// </summary>
        /// <param name="endpoint">The endpoint to validate.</param>
        public void Validate(ServiceEndpoint endpoint)
        {
            // No implementation required
            return;
        }

        #endregion


        #region IDispatchMessageInspector Members

        /// <summary>
        /// Called after an inbound message has been received but before the message is dispatched to the intended operation.
        /// </summary>
        /// <param name="request">The request message.</param>
        /// <param name="channel">The incoming channel.</param>
        /// <param name="instanceContext">The current service instance.</param>
        /// <returns>
        /// The object used to correlate state. This object is passed back in the <see cref="M:System.ServiceModel.Dispatcher.IDispatchMessageInspector.BeforeSendReply(System.ServiceModel.Channels.Message@,System.Object)"/> method.
        /// </returns>
        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

            int pos = request.Headers.FindHeader("Tenant", "Tenant");
            string tenantId = String.Empty;

            // BasicHttpBinding will put the token at pos=0, while wsHttpBinding will put it > 0
            // If the header is not found, the pos will be -1
            if (pos >= 0)
            {
                XmlDictionaryReader reader = request.Headers.GetReaderAtHeader(pos);
                // Read it through its static method ReadHeader
                tenantId = reader.ReadElementString();
                //Caching the TenantId
                HttpContext.Current.Session["TenantSession"] = tenantId;
               
                //Hashtable hashTenants = DataContractLibrary.TenantData.InMemory.TenantSessionMap;

                //if (!hashTenants.Contains(threadId))
                //{
                //    hashTenants.Add(threadId, tenantId);
                //}               
               
            }
            else
            {
                tenantId = "1";
                //Caching the TeanantId
                //Hashtable hashTenants = DataContractLibrary.TenantData.InMemory.TenantSessionMap;

                //if (!hashTenants.Contains(threadId))
                //{
                //    hashTenants.Add(threadId, tenantId);
                //}

                HttpContext.Current.Session["TenantSession"] = tenantId;
            }
            return null;
        }

        /// <summary>
        /// Called after the operation has returned but before the reply message is sent.
        /// </summary>
        /// <param name="reply">The reply message. This value is null if the operation is one way.</param>
        /// <param name="correlationState">The correlation object returned from the <see cref="M:System.ServiceModel.Dispatcher.IDispatchMessageInspector.AfterReceiveRequest(System.ServiceModel.Channels.Message@,System.ServiceModel.IClientChannel,System.ServiceModel.InstanceContext)"/> method.</param>
        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
           
        }

        #endregion

        #region Private methods
     
        private string GetServiceName(Uri endPoint)
        {
            string serviceName = string.Empty;
            if (endPoint != null && endPoint.AbsolutePath != null)
            {
                string endPointPath = endPoint.AbsolutePath;
                if (endPointPath.Contains(".svc"))
                {
                    int position = endPointPath.LastIndexOf("/");
                    if (position != -1)
                        serviceName = endPointPath.Substring(position + 1);
                }
            }
            return serviceName;
        }

        private string GetUuid(MessageHeaders msgHeader, bool isRequest)
        {
            string uuid = string.Empty;
            if (isRequest)
                uuid = Convert.ToString(msgHeader.MessageId);
            else
                uuid = Convert.ToString(msgHeader.RelatesTo);
            return uuid;
        }

        #endregion

    }
}
