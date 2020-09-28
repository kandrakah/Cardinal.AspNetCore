using System.Collections.Generic;
using System.Linq;

namespace Cardinal.AspNetCore.BackgroundServices.Configuration
{
    public class BackgroundServicesConfiguration : Dictionary<string, string>
    {
        public string GetCronExpression(string serviceName)
        {
            return this[serviceName];
        }
    }
}
