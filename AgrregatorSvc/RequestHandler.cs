using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrregatorSvc
{
    public abstract class RequestHandler
    {
        abstract public void ProcessData();
    }
}