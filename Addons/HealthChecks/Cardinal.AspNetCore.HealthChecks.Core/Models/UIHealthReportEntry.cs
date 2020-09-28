using System;
using System.Collections.Generic;

namespace Cardinal.AspNetCore.HealthChecks
{
    public class UIHealthReportEntry
    {
        public IReadOnlyDictionary<string, object> Data { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Exception { get; set; }
        public UIHealthStatus Status { get; set; }
    }
}
