using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Cardinal.Extensions
{
    /// <summary>
    /// SqlServer configuration
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// ASP.NET Identity Context config
        /// </summary>
        public static IServiceCollection PersistStore<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> databaseConfig) where TContext : DbContext
        {
            if (services.All(x => x.ServiceType != typeof(TContext)))
            {
                services.AddDbContext<TContext>(databaseConfig);
            }

            return services;
        }
    }
}
