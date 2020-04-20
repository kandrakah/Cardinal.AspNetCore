using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cardinal.AspNetCore.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão que adiciona o serviço padrão de autorização.
        /// </summary>
        /// <param name="services">Instância do container de serviços.</param>
        public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
        {
            services.AddAuthorizationService<DefaultAuthorizationService>();
            return services;
        }

        /// <summary>
        /// Extensão que adiciona o serviço de autorização.
        /// </summary>
        /// <typeparam name="TAuthorizationService"></typeparam>
        /// <param name="services">Instância do container de serviços.</param>
        public static IServiceCollection AddAuthorizationService<TAuthorizationService>(this IServiceCollection services) where TAuthorizationService : class, IAuthorizationService
        {
            services.AddScoped<IAuthorizationService, TAuthorizationService>();
            return services;
        }

        /// <summary>
        /// Método que registra os serviços no container de serviços da aplicação.
        /// </summary>
        /// <param name="services">Instância do container de serviços.</param>
        /// <param name="configuration">Instância do container de serviços.</param>
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
