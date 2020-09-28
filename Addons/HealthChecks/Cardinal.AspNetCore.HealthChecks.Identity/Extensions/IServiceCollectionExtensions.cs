using Cardinal.AspNetCore.Exceptions;
using Cardinal.AspNetCore.HealthChecks.Identity;
using Cardinal.AspNetCore.HealthChecks.Identity.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Extensão para adição da checagem de disponibilidade do serviço OpenId.
        /// </summary>
        /// <param name="builder">Objeto referenciado.</param>
        /// <param name="identityUri">Uri do servidor de identitdade.</param>
        /// <param name="name">Nome atribuído ao teste.</param>
        /// <param name="failureStatus">Resutado esperado em caso de falhas.</param>
        /// <param name="tags"></param>
        /// <param name="timeout"></param>
        /// <returns>Objeto referenciado.</returns>
        public static IHealthChecksBuilder AddOpenIdCheck(this IHealthChecksBuilder builder, Uri identityUri, string name = default, HealthStatus? failureStatus = default, IEnumerable<string> tags = default, TimeSpan? timeout = default)
        {
            var registrationName = name ?? "OpenId";

            if (identityUri == null)
            {
                throw new HealthCheckException(Resource.ERROR_INVALID_URI);
            }

            builder.Services.AddHttpClient(registrationName, client => client.BaseAddress = identityUri);
            return builder.Add(new HealthCheckRegistration(registrationName, sp => new IdentityServerHealthCheck(() => sp.GetRequiredService<IHttpClientFactory>().CreateClient(registrationName)), failureStatus, tags, timeout));
        }
    }
}
