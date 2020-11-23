using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Ocelot
{
    /// <summary>
    /// Swagger for Ocelot middleware.
    /// This middleware generate swagger documentation from downstream services for SwaggerUI.
    /// </summary>
    public class OcelotSwaggerMiddleware
    {
        private readonly RequestDelegate Next;

        private readonly IOptions<List<ReRouteOptions>> ReRoutes;
        private readonly Lazy<Dictionary<string, SwaggerEndPointOptions>> SwaggerEndPoints;
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly ISwaggerJsonTransformer Transformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="OcelotSwaggerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next delegate.</param>
        /// <param name="options">The options.</param>
        /// <param name="reRoutes">The Ocelot ReRoutes configuration.</param>
        /// <param name="swaggerEndPoints">The swagger end points.</param>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="transformer"></param>
        public OcelotSwaggerMiddleware(RequestDelegate next, SwaggerForOCelotUIOptions options, IOptions<List<ReRouteOptions>> reRoutes, IOptions<List<SwaggerEndPointOptions>> swaggerEndPoints, IHttpClientFactory httpClientFactory, ISwaggerJsonTransformer transformer)
        {
            this.Transformer = Check.NotNull(transformer, nameof(transformer));
            this.Next = Check.NotNull(next, nameof(next));
            this.ReRoutes = Check.NotNull(reRoutes, nameof(reRoutes));
            Check.NotNull(swaggerEndPoints, nameof(swaggerEndPoints));
            this.HttpClientFactory = Check.NotNull(httpClientFactory, nameof(httpClientFactory));

            var value = swaggerEndPoints.Value;

            SwaggerEndPoints = new Lazy<Dictionary<string, SwaggerEndPointOptions>>(() => value.ToDictionary(p => $"/{p.KeyToPath}", p => p));
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context)
        {
            var endPoint = GetEndPoint(context.Request.Path);
            var httpClient = HttpClientFactory.CreateClient();
            var content = await httpClient.GetStringAsync(endPoint.Url);
            content = Transformer.Transform(content, ReRoutes.Value.Where(p => p.SwaggerKey == endPoint.Key));
            await context.Response.WriteAsync(content);
        }

        private SwaggerEndPointOptions GetEndPoint(string path) => SwaggerEndPoints.Value[path];
    }
}
