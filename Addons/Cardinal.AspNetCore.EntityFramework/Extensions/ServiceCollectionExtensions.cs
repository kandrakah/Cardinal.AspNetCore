using Cardinal.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão que adiciona um contexto ao serviço.
        /// </summary>
        public static IServiceCollection PersistStore<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> databaseConfig) where TContext : DbContext
        {
            if (services.All(x => x.ServiceType != typeof(TContext)))
            {
                services.AddDbContext<TContext>(databaseConfig);
            }

            return services;
        }

        /// <summary>
        /// Extensão que adiciona a unidade de trabalho ao serviço.
        /// </summary>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        /// <summary>
        /// Extensão que adiciona a unidade de trabalho ao serviço.
        /// </summary>
        /// <typeparam name="TContext">Contexto utilizado pela unidade de trabalho.</typeparam>
        /// <param name="services">Objeto referenciado.</param>
        /// <returns>Objeto referenciado.</returns>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            return services;
        }
    }
}
