using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountInfo
{
    class UniqueIdGenerator
    {
        public static string GetUniqueId()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}
