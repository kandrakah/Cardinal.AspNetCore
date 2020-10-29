using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="HttpRequest"/>.
    /// </summary>
    public static partial class HttpRequestExtensions
    {
        /// <summary>
        /// Método que busca o token de autorização da requisição.
        /// </summary>
        /// <param name="request">Contexto de requisição.</param>
        /// <param name="type">Tipo de token esperado.</param>
        /// <returns>Token de autenticação ou null se o valor for vazio.</returns>
        public static string GetAuthorizationToken(this HttpRequest request, string type = "Bearer")
        {
            try
            {
                var authorization = request.Headers["Authorization"].FirstOrDefault();
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
