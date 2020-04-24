namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para Integer.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Extensão que verifica se o valor está entre dois valores informados.
        /// </summary>
        /// <param name="value">Exte valor.</param>
        /// <param name="minValue">Valor mínimo.</param>
        /// <param name="MaxValue">Valor máximo.</param>
        /// <param name="includeLimits">Indica se os valores das extremidades devem ser incluidos na verificação.</param>
        /// <returns>Resultado da verificação.</returns>
        public static bool IsBetween(this int value, int minValue, int MaxValue, bool includeLimits = true)
        {
            if (includeLimits)
            {
                return value >= minValue && value <= MaxValue;
            }
            else
            {
                return value > minValue && value < MaxValue;
            }
        }
    }
}
