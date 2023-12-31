using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace ClientInfrastructure.Wcf
{
    /// <summary>
    /// Class to intercept the call and insert credential in the message header
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CredentialsInterceptor<T> : InterceptorClientBase<T> where T : class
    {
        internal override void PreInvoke(ref Message request)
        {
            // Assign the token to the header
            MessageHeader header = MessageHeader.CreateHeader("Tenant","",1);

            request.Headers.Add(header);
        }
    }
}
