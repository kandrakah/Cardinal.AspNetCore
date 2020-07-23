using Cardinal.Extensions;
using Cardinal.Utils.Localization;
using System.Globalization;

namespace Cardinal.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class StatusCodeUtils
    {
        /// <summary>
        /// Método que traz a descrição de um código de status.
        /// </summary>
        /// <param name="statusCode">Código de status</param>
        /// <param name="culture">Dados do idioma desejado</param>
        /// <returns>Descrição do código de status</returns>
        public static string GetDescription(int statusCode, CultureInfo culture = null)
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
