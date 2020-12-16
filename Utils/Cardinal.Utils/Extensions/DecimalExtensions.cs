using Cardinal.Utils.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="decimal"/>.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Extensão que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static decimal Sum([NotNull] this decimal[] values)
        {
            var result = 0M;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Extensão que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static decimal Sum([NotNull] this IEnumerable<decimal> values)
        {
            var result = 0M;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

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

        /// <summary>
        /// Extensão que efetua o cálculo para descobrir quanto o valor atual
        /// é em porcentagem de um valor total.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="total">Valor de referência total.</param>
        /// <param name="decimals">Quantidade de casas decimais desejadas.</param>
        /// <returns>Valor percentual resultante do cálculo.</returns>
        public static decimal AsPercentOf(this decimal value, decimal total, int decimals = 2)
        {
            var result = (value / total * 100);
            return Math.Round(result, decimals);
        }

        /// <summary>
        /// Extensão que efetua o cálculo para descobrir quanto informado é
        /// em porcentagem do valor atual.
        /// </summary>
        /// <param name="value">Este valor.</param>
        /// <param name="total">Valor de referência total.</param>
        /// <param name="decimals">Quantidade de casas decimais desejadas.</param>
        /// <returns>Valor percentual resultante do cálculo.</returns>
        public static decimal AsPercentFrom(this decimal total, decimal value, int decimals = 2)
        {
            return value.AsPercentOf(total, decimals);
        }
    }
}
