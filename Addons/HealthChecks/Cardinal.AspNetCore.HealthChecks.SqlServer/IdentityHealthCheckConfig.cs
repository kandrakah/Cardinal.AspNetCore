using Cardinal.AspNetCore.HealthChecks.Core;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cardinal.AspNetCore.HealthChecks.SqlServer
{
    public class IdentityHealthCheckConfig : HealthCheckConfig
    {
        public string ConnectionString { get; set; }

        public string Query { get; set; }
        
    }
}
