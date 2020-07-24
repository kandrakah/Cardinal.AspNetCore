using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text.RegularExpressions;

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
    }
}
