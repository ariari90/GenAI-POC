using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAuthSite
{
    public class AggregatorResponseDataAccess
    {
        public static AggregatorResponse response { get; set; }

        public static AggregatorResponse GetResponse()
        {
            return response;
        }
    }
}