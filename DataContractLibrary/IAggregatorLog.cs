using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface IAggregatorLog
    {
        void LogMessage(string message);
        void LogError(string message);
    }
}
