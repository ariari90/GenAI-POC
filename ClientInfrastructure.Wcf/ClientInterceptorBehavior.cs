using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Xml;
using System.Runtime.Serialization;
using System.Web;
using System.Collections;

namespace ClientInfrastructure.Wcf
{
    /// <summary>
    /// Endpoint behavior to add credential
    /// </summary>
    public class ClientInterceptorBehavior : BehaviorExtensionElement, IEndpointBehavior, IClientMessageInspector
    {

        /// <summary>
        /// Read only property to obtain the type of behaviour
        /// </summary>
        public override Type BehaviorType
        {

            get { return typeof(ClientInterceptorBehavior); }
        }

        /// <summary>
        ///  Creates a behavior extension based on the current configuration settings.
        ///  Overridden from base to return the custom behaviour
        /// </summary>
        /// <returns>Tyhe custom behaviour object.</returns>
        protected override object CreateBehavior()
        {
            return new ClientInterceptorBehavior();
        }

        #region IEndpointBehavior Members

        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="endpoint">The endpoint to modify.</param>
        /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
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


        #endregion

        #region IClientMessageInspector Members

        /// <summary>
        /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
        /// </summary>
        /// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
        /// <param name="correlationState">Correlation state data.</param>
        void IClientMessageInspector.AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
           
        }

        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// </summary>
        /// <param name="request">The message to be sent to the service.</param>
        /// <param name="channel">The WCF client object channel.</param>
        /// <returns>
        /// The object that is returned as the <paramref name="correlationState "/>argument of the <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)"/> method. This is null if no correlation state is used.The best practice is to make this a <see cref="T:System.Guid"/> to ensure that no two <paramref name="correlationState"/> objects are the same.
        /// </returns>
        object IClientMessageInspector.BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            string tenantId = string.Empty;
            string sessionToken = string.Empty;

            if (HttpContext.Current.Session["TenantSession"] != null)
                tenantId = HttpContext.Current.Session["TenantSession"].ToString();

            //int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            ////Caching the TeanantId
            //Hashtable hashTenants = DataContractLibrary.TenantData.InMemory.TenantSessionMap;
            //if (hashTenants.Contains(threadId))
            //    tenantId = hashTenants[threadId].ToString();           

            MessageHeader tenantHeader = MessageHeader.CreateHeader("Tenant", "Tenant", tenantId);

            request.Headers.Add(tenantHeader);
            return null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the soap message to find the node. If found than deserialize it using the
        /// NetDataContractSerializer to construct the exception.
        /// </summary>
        /// <param name="reply">The reply message</param>
        /// <returns></returns>
        private static object ReadFaultDetail(Message reply)
        {
            const string detailElementName = "detail";
            using (var reader = reply.GetReaderAtBodyContents())
            {
                // Find<detail>
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element &&
                    reader.LocalName.ToLower() == detailElementName)
                    {
                        break;
                    }
                }

                if (reader.NodeType != XmlNodeType.Element ||
                reader.LocalName.ToLower() != detailElementName)
                {
                    return null;
                }

                // Move to the contents of <soap:Detail>
                if (!reader.Read())
                {
                    return null;
                }

                // Deserialize the fault
                var serializer = new NetDataContractSerializer();
                Object faultObject = null;
                //check if it is typed fault execption by trying to deserialize it
                try
                {
                    faultObject = serializer.ReadObject(reader);
                }
                //return null if it is a typed fault exception
                catch (Exception)
                {
                    faultObject = null;
                }
                return faultObject;
            }
        }

        #endregion

        //#region Private methods for logging
        //private string GetLogMessage(MessageHeaders header, int? cisId, bool isRequest, bool isFault, Exception exception)
        //{
        //    StringBuilder logMessage = new StringBuilder();
        //    if (isRequest)
        //    {
        //        logMessage.Append(Logging.REQUEST_LOG);
        //    }
        //    else
        //        logMessage.Append(Logging.RESPONSE_LOG);

        //    logMessage.Append(Logging.PIPE_DELIMITER);
        //    logMessage.Append(Logging.UUID_LOG);
        //    logMessage.Append(GetUuid(header, isRequest));
        //    logMessage.Append(Logging.PIPE_DELIMITER);

        //    logMessage.Append(Logging.OPERATION_LOG);
        //    logMessage.Append(header.Action);

        //    if (!isRequest)
        //    {
        //        logMessage.Append(Logging.PIPE_DELIMITER);
        //        logMessage.Append(Logging.FAULT_LOG);
        //        logMessage.Append(isFault ? "Yes" : "No");
        //        if (isFault)
        //        {
        //            if (exception != null)
        //            {
        //                logMessage.Append(Logging.PIPE_DELIMITER);
        //                logMessage.Append(GetExceptionMessage(exception));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        logMessage.Append(Logging.PIPE_DELIMITER);
        //        logMessage.Append(Logging.CIS_ID);
        //        logMessage.Append(cisId == null ? string.Empty : Convert.ToString(cisId));
        //    }
        //    return logMessage.ToString();
        //}

        //private string GetUuid(MessageHeaders msgHeader, bool isRequest)
        //{
        //    string uuid = string.Empty;
        //    if (isRequest)
        //        uuid = Convert.ToString(msgHeader.MessageId);
        //    else
        //        uuid = Convert.ToString(msgHeader.RelatesTo);
        //    return uuid;
        //}

        //private string GetExceptionMessage(Exception exception)
        //{
        //    StringBuilder exceptionMsg = new StringBuilder();
        //    exceptionMsg.Append(Logging.EXCEPTION_DETAILS);
        //    exceptionMsg.Append(Logging.PIPE_DELIMITER);

        //    exceptionMsg.Append(Logging.EXCEPTION_TYPE);
        //    exceptionMsg.Append(exception.GetType().AssemblyQualifiedName);
        //    exceptionMsg.Append(Logging.PIPE_DELIMITER);

        //    exceptionMsg.Append(Logging.EXCEPTION_MESSAGE);
        //    exceptionMsg.Append(exception.Message);
        //    exceptionMsg.Append(Logging.PIPE_DELIMITER);

        //    exceptionMsg.Append(Logging.EXCEPTION_SOURCE);
        //    exceptionMsg.Append(exception.Source);
        //    exceptionMsg.Append(Logging.PIPE_DELIMITER);

        //    exceptionMsg.Append(Logging.EXCEPTION_STACKTRACE);
        //    string stackTrace = string.Empty;
        //    if (exception.StackTrace != null)
        //        stackTrace = exception.StackTrace.Replace("\r\n", ",");
        //    exceptionMsg.Append(stackTrace);

        //    return exceptionMsg.ToString();
        //}

        //#endregion

    }
}
