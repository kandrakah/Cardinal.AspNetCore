using Cardinal.Utils.Localization;
using System.Globalization;
using System.Net;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="HttpStatusCode"/>.
    /// </summary>
    public static class HttpStatusCodeExtensions
    {        

        /// <summary>
        /// Método que obtém a descrição de um código de status.
        /// </summary>
        /// <param name="statusCode">Objeto referenciado</param>
        /// <param name="culture">Dados do idioma desejado</param>
        /// <returns>Descrição do código de status</returns>
        public static string GetDescription(this HttpStatusCode statusCode, CultureInfo culture = null)
        {
            return GetDescription((int)statusCode, culture);
        }

        /// <summary>
        /// Método que obtém a descrição de um código de status.
        /// </summary>
        /// <param name="statusCode">Código de status o qual se deseja a descrição</param>
        /// <param name="culture">Dados do idioma desejado</param>
        /// <returns>Descrição do código de status</returns>
        private static string GetDescription(int statusCode, CultureInfo culture = null)
        {
            if (culture == null)
            {
                culture = Resource.Culture;
            }
            var value = Resource.ResourceManager.GetString($"STATUS_CODE_{statusCode}", culture);
            return string.IsNullOrEmpty(value) ? Resource.STATUS_CODE_UNKNOW.SetParameters("VALUE", statusCode) : value;
        }
    }
}
