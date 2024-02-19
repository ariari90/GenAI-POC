using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AggregatorSvcService
{
    public abstract class RequestHandler
    {
        abstract public AggregatorResponse ProcessData();
    }
}