using Cardinal.AspNetCore.Utils;
using System.Net;

namespace Cardinal.AspNetCore.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        /// <summary>
        /// Método que obtém a descrição de um código de status.
        /// </summary>
        /// <param name="statusCode">Código de status o qual se dejesa a descrição.</param>
        /// <returns>Descrição do código de status.</returns>
        public static string GetDescription(this HttpStatusCode statusCode)
        {
            return StatusCodeUtils.GetDescription(statusCode);
        }

        /// <summary>
        /// Método que obtém a descrição de um código de status.
        /// </summary>
        /// <param name="statusCode">Código de status o qual se dejesa a descrição.</param>
        /// <param name="includeStatusCode">Indica se deve ser adicionado o código de erro à descrição.</param>
        /// <returns>Descrição do código de status.</returns>
        public static string GetDescription(this HttpStatusCode statusCode, bool includeStatusCode = false)
        {
            return includeStatusCode ? $"{(int)statusCode}:{StatusCodeUtils.GetDescription(statusCode)}" : StatusCodeUtils.GetDescription(statusCode);
        }
    }
}
