using System;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para Guid.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Extensão que faz a comparação entre dois Guids para verificar sua igualdade.
        /// </summary>
        /// <param name="value">Este Guid.</param>
        /// <param name="comparedValue">Guid à ser comparado.</param>
        /// <returns>Resultado da comparação.</returns>
        public static bool IsEquals(this Guid value, Guid comparedValue)
        {
            return value.Equals(comparedValue);
        }

        /// <summary>
        /// Extensão que faz a comparação entre dois Guids para verificar sua igualdade.
        /// </summary>
        /// <param name="value">Este Guid.</param>
        /// <param name="comparedValue">Guid à ser comparado.</param>
        /// <param name="ignoreIfNull">Ignorar verificação se o valor Guid atual for nulo.</param>
        /// <returns>Resultado da comparação.</returns>
        public static bool IsEquals(this Guid? value, Guid? comparedValue, bool ignoreIfNull = false)
        {
            if (ignoreIfNull && value == null)
            {
                return true;
            }
            else
            {
                return value.Equals(comparedValue);
            }
        }

        /// <summary>
        /// Extensão que transforma este Guid em uma representação string em caixa alta.
        /// </summary>
        /// <param name="guid">Este Objeto.</param>
        /// <returns>Representação String em caixa alta.</returns>
        public static string ToUpper(this Guid guid)
        {
            return guid.ToString().ToUpper();
        }
    }
}
