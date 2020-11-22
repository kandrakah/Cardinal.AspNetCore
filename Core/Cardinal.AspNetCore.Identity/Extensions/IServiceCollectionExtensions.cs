using Cardinal.AspNetCore;
using Cardinal.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetConfigurations<IdentityConfigurations>("Identity");

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication("token")
                .AddJwtBearer("token", options =>
                {
                    options.Authority = settings.Authority;
                    options.Audience = settings.Audience;

                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    options.TokenValidationParameters.ValidateAudience = settings.ValidateAudience;
                    options.TokenValidationParameters.ValidateIssuer = settings.ValidateIssuer;
                    options.TokenValidationParameters.ValidateLifetime = settings.ValidateLifetime;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = settings.ValidateIssuerSigningKey;

                }).AddOAuth2Introspection("introspection", options =>
                {
                    options.Authority = settings.Authority;

                    options.ClientId = settings.ApiName;
                    options.ClientSecret = settings.ApiSecret;
                });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("tokens", p => p.AddAuthenticationSchemes("jwt", "introspection").RequireAuthenticatedUser());
            });

            return services;
        }


        /// <summary>
        /// Extensão que adiciona o serviço padrão de autorização.
        /// </summary>
        /// <param name="services">Instância do container de serviços.</param>
        public static IServiceCollection AddPermissionsAuthorization(this IServiceCollection services)
        {
            services.AddPermissionsAuthorization<DefaultPermissionsAuthorizationService>();
            services.AddSingleton<ISystemUser, IdentityUser>();
            return services;
        }

        /// <summary>
        /// Extensão que adiciona o serviço de autorização.
        /// </summary>
        /// <typeparam name="TAuthorizationService"></typeparam>
        /// <param name="services">Instância do container de serviços.</param>
        public static IServiceCollection AddPermissionsAuthorization<TAuthorizationService>(this IServiceCollection services) where TAuthorizationService : class, IPermissionsAuthorizationService
        {
            services.AddScoped<IPermissionsAuthorizationService, TAuthorizationService>();
            return services;
        }
    }
}
