using Cardinal.AspNetCore.Identity;
using Cardinal.AspNetCore.Identity.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
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
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetCurrentUserClaims(this AuthorizationFilterContext context, AuthoritySettings settings)
        {
            var token = context.GetAuthorizationToken("Bearer");
            var client = new AuthorityService(settings.Authority, token);
            var claims = client.GetUserClaimsAsync().Result;
            return claims;
        }
    }
}
