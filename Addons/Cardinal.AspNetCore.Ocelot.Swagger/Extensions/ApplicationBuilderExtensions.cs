using Cardinal.AspNetCore.Ocelot;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensions for adding <see cref="OcelotSwaggerMiddleware"/> into application pipeline.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Add Swagger generator for downstream services and UI into application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// <see cref="IApplicationBuilder"/>.
        /// </returns>
        public static IApplicationBuilder UseSwaggerForOcelotUI(this IApplicationBuilder app, IConfiguration configuration)
        {
            return app.UseSwaggerForOcelotUI(configuration, null);
        }

        /// <summary>
        /// Add Swagger generator for downstream services and UI into application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="setupAction">Setup <see cref="SwaggerForOCelotUIOptions"/></param>
        /// <returns>
        /// <see cref="IApplicationBuilder"/>.
        /// </returns>
        public static IApplicationBuilder UseSwaggerForOcelotUI(this IApplicationBuilder app, IConfiguration configuration, Action<SwaggerForOCelotUIOptions> setupAction)
        {
            var options = new SwaggerForOCelotUIOptions();
            setupAction?.Invoke(options);

            UseSwaggerForOcelot(app, options);

            app.UseSwaggerUI(c =>
            {
                InitUIOption(c, options);
                var endPoints = GetConfugration(configuration);
                AddSwaggerEndPoints(c, endPoints, options.EndPointBasePath);                
            });

            return app;
        }

        private static void UseSwaggerForOcelot(IApplicationBuilder app, SwaggerForOCelotUIOptions options)
        {
            app.Map(options.EndPointBasePath, builder => builder.UseMiddleware<OcelotSwaggerMiddleware>(options));
        }

        private static void AddSwaggerEndPoints(SwaggerUIOptions c, IEnumerable<SwaggerEndPointOptions> endPoints, string basePath)
        {
            if (endPoints == null)
            {
                return;
            }
            foreach (var endPoint in endPoints)
            {
                c.SwaggerEndpoint($"{basePath}/{endPoint.KeyToPath}", endPoint.Name);
            }
        }

        private static void InitUIOption(SwaggerUIOptions c, SwaggerForOCelotUIOptions options)
        {
            c.ConfigObject = options.ConfigObject;
            c.DocumentTitle = options.DocumentTitle;
            c.HeadContent = options.HeadContent;
            c.IndexStream = options.IndexStream;
            c.OAuthConfigObject = options.OAuthConfigObject;
            c.RoutePrefix = options.RoutePrefix;
        }

        private static IEnumerable<SwaggerEndPointOptions> GetConfugration(IConfiguration configuration)
        {
            return configuration.GetSection(SwaggerEndPointOptions.ConfigurationSectionName).Get<IEnumerable<SwaggerEndPointOptions>>();
        }
    }
}
