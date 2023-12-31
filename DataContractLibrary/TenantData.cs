using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace DataContractLibrary
{
    public sealed class TenantData
    {
        static readonly TenantData Instance = new TenantData    (); 

        private TenantData() { }

        private Hashtable tenantSessionMap = new Hashtable();

        public static TenantData InMemory
        {
            get
            {
                return Instance;
            }
        }

        public Hashtable TenantSessionMap
        {
            get { return tenantSessionMap; }
            set { tenantSessionMap = value; }
        }
    }
}
