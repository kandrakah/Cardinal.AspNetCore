using Cardinal.AspNetCore;
using Cardinal.AspNetCore.Identity;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetConfigurations<AuthorityConfigurations>("Authority");

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

                    //options.ForwardDefaultSelector = Selector.ForwardReferenceToken("introspection");
                }).AddOAuth2Introspection("introspection", options =>
                {
                    options.Authority = settings.Authority;

                    options.ClientId = settings.ApiName;
                    options.ClientSecret = settings.ApiSecret;
                });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = settings.Authority;
            //        options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
            //        options.ApiSecret = settings.ApiSecret;
            //        options.ApiName = settings.ApiName;
            //        options.RoleClaimType = settings.RoleClaimType;
            //        options.NameClaimType = settings.NameClaimType;
            //        options.SaveToken = settings.SaveToken;
            //        options.SupportedTokens = settings.SupportedTokens;
            //    });

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
        public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
        {
            services.AddAuthorizationService<DefaultAuthorizationService>();
            services.AddSingleton<ISystemUser, IdentityUser>();
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
