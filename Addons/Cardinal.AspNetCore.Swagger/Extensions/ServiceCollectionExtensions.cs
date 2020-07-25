using Cardinal.AspNetCore.Swagger;
using Cardinal.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cardinal.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, string section = SwaggerConstants.SWAGGER_SECTION)
        {
            var settings = configuration.GetSettings<SwaggerSettings>(section);
            return services.AddSwagger(settings);
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerSettings settings)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(settings.Version, settings.ToOpenApi());

                if (settings.UseSecurity)
                {
                    setup.AddSecurityDefinition(settings.SecurityDefinitions.Scheme, settings.SecurityDefinitions.ToOpenApi());
                    setup.AddSecurityRequirement(settings.SecurityRequeriment.ToOpenApi());
                }
            });
            return services;
        }        
    }
}
