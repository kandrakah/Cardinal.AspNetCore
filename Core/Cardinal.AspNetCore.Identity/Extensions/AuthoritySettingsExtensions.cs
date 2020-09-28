using Cardinal.AspNetCore.Identity;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="AuthorityConfigurations"/>.
    /// </summary>
    public static class AuthoritySettingsExtensions
    {
        /// <summary>
        /// Extensão que retorna os parâmetros de validação de token OAuth.
        /// </summary>
        /// <param name="settings">Objeto referenciado</param>
        /// <param name="token">Token de autenticação</param>
        /// <returns>Parâmetros de validação do Token. Veja <see cref="TokenValidationParameters"/></returns>
        internal static async Task<TokenValidationParameters> GetTokenParametesAsync(this AuthorityConfigurations settings, JwtSecurityToken token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = settings.ValidateIssuerSigningKey,
                    IssuerSigningKey = await settings.GetSigningKeyAsync(token),
                    ValidateIssuer = settings.ValidateIssuer,
                    ValidateAudience = settings.ValidateAudience,
                    ValidateLifetime = settings.ValidateLifetime,
                    ClockSkew = TimeSpan.Zero
                };

                return tokenValidationParameters;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Método para obtenção da chave de assinatura do token provida pela autoridade de identificação.
        /// </summary>
        /// <param name="settings">Objeto referenciado</param>
        /// <param name="token">Token de autenticação</param>
        /// <returns>Chave de segurança provida pela autoridade de identificação.</returns>
        private static async Task<SecurityKey> GetSigningKeyAsync(this AuthorityConfigurations settings, JwtSecurityToken token)
        {
            var client = new HttpClient();
            try
            {
                var discovery = await client.GetDiscoveryDocumentAsync(settings.Authority);
                var kid = token.Header.Kid;

                var key = discovery.KeySet.Keys.Where(x => x.Kid == kid).FirstOrDefault();

                var json = JsonConvert.SerializeObject(key);

                var result = JsonWebKey.Create(json);
                //var keys = client.GetRequest(".well-known/openid-configuration/jwks").ExecuteAsync<IdentityKeySet>().Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
