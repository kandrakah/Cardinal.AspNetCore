using System;
using System.Collections.Generic;
using System.Linq;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="Enum"/>.
    /// </summary>
    public static class EnumeratorExtensions
    {
        /// <summary>
        /// Extensão para converter um enumerador em um dicionário.
        /// </summary>
        /// <param name="enumerator">Objeto referenciado</param>
        /// <returns>Dicionário resultante do enumerador</returns>
        public static IDictionary<int, string> ToDictionary(this Enum enumerator)
        {
            var type = enumerator.GetType();
            var values = Enum.GetValues(type).Cast<Enum>().ToDictionary(t => (int)(object)t, t => t.ToString());
            return values;
        }
    }
}
