using Microsoft.AspNetCore.Builder;
using Serilog.Context;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Extens serilog logs with additional information like remote IP
        /// address
        /// </summary>
        /// <param name="app">Instance of <see cref="IApplicationBuilder"/>.
        /// </param>
        public static void UseSerilog(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                var remoteIpAddress = ctx.Request.HttpContext.Connection.RemoteIpAddress;

                using (LogContext.PushProperty("RemoteIpAddress", remoteIpAddress))
                {
                    await next();
                }
            });
        }
    }
}
