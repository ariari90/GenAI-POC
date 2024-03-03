using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class OrchestratorConfig
    {
        public List<Orchestrator> Orchestrators { get; set; }
    }

    public class Orchestrator
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
    }
}
