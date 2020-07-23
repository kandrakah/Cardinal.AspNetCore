using Cardinal.Utils.Localization;
using System.Globalization;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="decimal"/>.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Extensão para transformar o valor decimal em sua representação monetária.
        /// </summary>
        /// <param name="value">Objeto referenciado</param>
        /// <returns>Representação monetária do valor decimal</returns>
        public static string AsCurrency(this decimal value)
        {
            return string.Format(Resource.Culture, "{0:C}", value);
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em sua representação monetária.
        /// </summary>
        /// <param name="value">Este valor</param>
        /// <returns>Representação monetária do valor decimal</returns>
        public static string AsCurrency(this decimal? value)
        {
            return value != null ? string.Format(Resource.Culture, "{0:C}", value) : string.Format(Resource.Culture, "{0:C}", 0);
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em sua representação monetária.
        /// </summary>
        /// <param name="value">Este valor</param>
        /// <param name="culture">Informações de idioma à serem usados na conversão</param>
        /// <returns>Representação monetária do valor decimal</returns>
        public static string AsCurrency(this decimal value, CultureInfo culture)
        {
            return string.Format(culture, "{0:C}", value);
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em sua representação monetária.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="culture">Informações de idioma à serem usados na conversão.</param>
        /// <returns>Representação monetária do valor decimal.</returns>
        public static string AsCurrency(this decimal? value, CultureInfo culture)
        {
            return value != null ? string.Format(culture, "{0:C}", value) : string.Format(culture, "{0:C}", 0);
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em porcentagem.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="transform">Indica se o valor deve ser normalizado.</param>
        /// <returns>Representação em porcentagem do valor decimal.</returns>
        public static string AsPercentage(this decimal value, bool transform = false)
        {
            return transform ? $"{value * 100}%" : $"{value}%";
        }

        /// <summary>
        /// Extensão para transformar o valor decimal em porcentagem.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="transform">Indica se o valor deve ser normalizado.</param>
        /// <returns>Representação em porcentagem do valor decimal.</returns>
        public static string AsPercentage(this decimal? value, bool transform = false)
        {
            return value != null ? transform ? $"{value * 100}%" : $"{value}%" : "0%";
        }
    }
}
