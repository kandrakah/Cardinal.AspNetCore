using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cardinal.AspNetCore.HealthChecks
{
    public abstract class HealthCheckConfig
    {
        public string Name { get; set; }

        public HealthStatus? FailureStatus { get; set; } = HealthStatus.Unhealthy;

        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();

        public TimeSpan? Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
