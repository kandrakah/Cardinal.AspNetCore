using System;

namespace Cardinal.AspNetCore.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsEquals(this Guid value, Guid comparedValue)
        {
            return value.Equals(comparedValue);
        }

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
        /// Método de extensão que transforma este Guid em uma representação string em caixa alta.
        /// </summary>
        /// <param name="guid">Este Objeto.</param>
        /// <returns>Representação String em caixa alta.</returns>
        public static string ToUpper(this Guid guid)
        {
            return guid.ToString().ToUpper();
        }
    }
}
