using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class AccountTransferSvcRequest
    {
        public bool ExecuteWorkflow { get; set; }
        public int AccountNumberUniqueId { get; set; }
        public int BusinessAccountUniqueId { get; set; }
        public int NewAccountNumber { get; set; }
        public int NewBusinessAccountNumber { get; set; }
    }
}
