using Cardinal.AspNetCore.HealthChecks.Identity.Localization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.HealthChecks.Identity
{
    /// <summary>
    /// Serviço de verificação de integridade do OpenId.
    /// </summary>
    public class IdentityServerHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Uri do endpoint de discovery.
        /// </summary>
        const string IDSVR_DISCOVER_CONFIGURATION_SEGMENT = ".well-known/openid-configuration";

        /// <summary>
        /// Instância de leitura da fábrica HTTP.
        /// </summary>
        private readonly Func<HttpClient> _httpClientFactory;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="httpClientFactory">Função para criação do Client HTTP.</param>
        public IdentityServerHealthCheck(Func<HttpClient> httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// Método que faz a checagem de disponibilidade do servidor OpenId.
        /// </summary>
        /// <param name="context">Contexto do verificador.</param>
        /// <param name="cancellationToken">Instância do token de cancelamento.</param>
        /// <returns>Retorno assíncrono do resultado da verificação.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory();
                var response = await httpClient.GetAsync(IDSVR_DISCOVER_CONFIGURATION_SEGMENT, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return new HealthCheckResult(context.Registration.FailureStatus, description: Resource.IDENTITY_DISCOVERY_UNHEALTHY.Replace("{code}", $"{(int)response.StatusCode}", StringComparison.InvariantCultureIgnoreCase).Replace("{content}", content, StringComparison.InvariantCultureIgnoreCase));
                }

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}
