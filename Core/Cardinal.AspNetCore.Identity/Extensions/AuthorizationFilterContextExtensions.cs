using Cardinal.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="AuthorizationFilterContext"/>.
    /// </summary>
    public static partial class AuthorizationFilterContextExtensions
    {
        /// <summary>
        /// Método que busca o token de autorização da requisição.
        /// </summary>
        /// <param name="context">Contexto.</param>
        /// <param name="type">Tipo de token esperado.</param>
        /// <returns>Token de autenticação ou null se o valor for vazio.</returns>
        public static string GetAuthorizationToken(this AuthorizationFilterContext context, string type = "Bearer")
        {
            try
            {
                var authorization = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var token = Regex.Replace(authorization, type, string.Empty, RegexOptions.IgnoreCase).Trim();
                return token;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Método que busca as claims do usuário atual.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configurations">Configurações do provedor de identidade.</param>
        /// <returns>Enumeração de claims do usuário atual.</returns>
        public static async Task<IEnumerable<Claim>> GetCurrentUserClaims(this AuthorizationFilterContext context, IdentityConfigurations configurations)
        {
            var token = context.GetAuthorizationToken("Bearer");
            var client = new HttpClient();
            var userInfo = await client.GetUserInfoAsync(configurations.Authority, token);
            var claims = userInfo.ToClaims();
            return claims;
        }
    }
}
