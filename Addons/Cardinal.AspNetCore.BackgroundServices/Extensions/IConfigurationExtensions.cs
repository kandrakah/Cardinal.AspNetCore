using Cardinal.AspNetCore.BackgroundServices.Configuration;
using Microsoft.Extensions.Configuration;

namespace Cardinal.Extensions
{
    public static class IConfigurationExtensions
    {
        public static BackgroundServicesConfiguration GetBackgroundServiceConfiguration(this IConfiguration configuration, string sectionName = "backgroundServices")
        {
            var result = configuration.GetSection(sectionName).Get<BackgroundServicesConfiguration>();
            return result;
        }
    }
}
