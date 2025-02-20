using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public enum RequestType
    {
        AccountInfo,
        HoldingsInfo,
        UpdateDetails,
        Transaction,
        AccountAndHoldings,
        Extra
    }
}
