using Microsoft.Extensions.DependencyInjection;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão para adição do usuário de sistema.
        /// </summary>
        /// <param name="services">Objeto referenciado</param>
        /// <returns>Objeto referenciado</returns>
        public static IServiceCollection AddSystemUser(this IServiceCollection services)
        {
            services.AddScoped<ISystemUser, SystemUser>();
            return services;
        }
    }
}
