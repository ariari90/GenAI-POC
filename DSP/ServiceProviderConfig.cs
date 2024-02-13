using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSP
{
    public class ServiceProviderConfig
    {
        public List<ServiceProvider> ServiceProviders;
    }

    public class ServiceProvider
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
