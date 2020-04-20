using Cardinal.AspNetCore.Localization;
using System.Globalization;
using System.Net;
using System.Threading;

namespace Cardinal.AspNetCore.Utils
{
    /// <summary>
    /// Classe de utilidades para códigos de status HTTP.
    /// </summary>
    public static class StatusCodeUtils
    {
        /// <summary>
        /// Método que obtém a descrição de um código de status.
        /// </summary>
        /// <param name="statusCode">Código de status o qual se dejesa a descrição.</param>
        /// <param name="culture">Dados do idioma desejado.</param>
        /// <returns>Descrição do código de status.</returns>
        public static string GetDescription(HttpStatusCode statusCode, CultureInfo culture = null)
        {
            return GetDescription((int)statusCode, culture);
        }

        /// <summary>
        /// Método que obtém a descrição de um código de status.
        /// </summary>
        /// <param name="statusCode">Código de status o qual se deseja a descrição.</param>
        /// <param name="culture">Dados do idioma desejado.</param>
        /// <returns>Descrição do código de status.</returns>
        public static string GetDescription(int statusCode, CultureInfo culture = null)
        {
            if(culture == null)
            {
                culture = Thread.CurrentThread.CurrentCulture;
            }
            var value = Resource.ResourceManager.GetString($"STATUS_CODE_{statusCode}", culture);
            return string.IsNullOrEmpty(value) ? ResourceUtils.Translate(Resource.STATUS_CODE_UNKNOW, ResourceUtils.Set("VALUE", statusCode)) : value;
        }
    }
}
