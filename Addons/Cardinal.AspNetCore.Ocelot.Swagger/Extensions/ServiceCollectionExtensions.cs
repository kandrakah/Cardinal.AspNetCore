using Cardinal.AspNetCore.Ocelot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;


namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensions for adding configuration for <see cref="OcelotSwaggerMiddleware"/> into <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds configuration for for <see cref="OcelotSwaggerMiddleware"/> into <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSwaggerForOcelot(this IServiceCollection services, IConfiguration configuration)
        {
            var routes = configuration.GetSection("routes");
            return services
                .AddTransient<ISwaggerJsonTransformer, SwaggerJsonTransformer>()
                .Configure<List<ReRouteOptions>>(options => routes.Bind(options))
                .Configure<List<SwaggerEndPointOptions>>(options => configuration.GetSection(SwaggerEndPointOptions.ConfigurationSectionName).Bind(options))
                .AddHttpClient();
        }
    }
}
