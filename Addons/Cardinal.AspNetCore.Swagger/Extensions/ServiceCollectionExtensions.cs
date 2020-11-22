using Cardinal.AspNetCore.Swagger;
using Cardinal.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Cardinal.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, string section = SwaggerConstants.SWAGGER_SECTION)
        {
            var configurations = configuration.GetConfigurations<SwaggerConfigurations>(section);
            return services.AddSwagger(configurations);
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerConfigurations configurations)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(configurations.Version, configurations.ToOpenApi());

                foreach (var file in Directory.GetFiles(configurations.DocumentationPath, configurations.DocumentationPattern))
                {
                    setup.IncludeXmlComments(file, configurations.IncludeControllerXmlComments);
                }

                if (configurations.UseSecurity)
                {
                    setup.OperationFilter<AuthorizeCheckOperationFilter>();
                    setup.AddSecurityDefinition(configurations.SecurityDefinitions.Scheme, configurations.SecurityDefinitions.ToOpenApi());
                    setup.AddSecurityRequirement(configurations.SecurityRequeriment.ToOpenApi());
                }
            });
            return services;
        }        
    }
}
