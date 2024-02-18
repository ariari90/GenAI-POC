using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AuthenticationService
{
    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        bool Authenticate(AuthInfo authInfo);
    }
}
