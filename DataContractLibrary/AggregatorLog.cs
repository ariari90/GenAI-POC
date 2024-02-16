//using Microsoft.Extensions.Logging;
//using Serilog;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Common
{
    public class AggregatorLog<T>: IAggregatorLog
    {
        private ILog log;

        public AggregatorLog()
        {
            log = GetLogger();
        }

        private ILog GetLogger()
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            return log;
        }

        public void LogMessage(string message)
        {
            log.Debug(message);
        }

        public void LogError(string message)
        {
            log.Error(message);
        }
        
    }
}
