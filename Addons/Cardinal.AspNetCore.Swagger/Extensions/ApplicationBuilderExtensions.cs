using Cardinal.AspNetCore.Swagger;
using Cardinal.Extensions;
using Cardinal.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Cardinal.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder builder, IConfiguration configuration, string section = SwaggerConstants.SWAGGER_SECTION)
        {
            var settings = configuration.GetConfigurations<SwaggerSettings>(section);
            return builder.UseSwagger(settings);
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder builder, SwaggerSettings settings)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(settings.GetEndpointUri(), settings.Title);
                options.OAuthAppName(settings.AuthAppName);
                options.OAuthClientId(settings.ClientId);
                options.OAuthClientSecret(settings.Secret);

                if (settings.UsePkce)
                {
                    options.OAuthUsePkce();
                }
            });

            return builder;
        }
    }
}
