using Cardinal.AspNetCore.IdentityServer.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Cardinal.Extensions
{
    public static class IServiceCollectionExtensions
    {
        // <summary>
        /// Creates a builder.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IPasswordHashBuilder UseCustomHashPasswordBuilder(this IServiceCollection services)
        {
            return new PasswordHasherBuilder(services);
        }

        public static IPasswordHashBuilder UpgradePasswordSecurity(this IServiceCollection services)
        {
            return services.UseCustomHashPasswordBuilder();
        }
    }
}
