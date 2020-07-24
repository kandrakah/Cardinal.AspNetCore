using Cardinal.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IServiceCollection"/>.
    /// </summary>
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
    }
}
