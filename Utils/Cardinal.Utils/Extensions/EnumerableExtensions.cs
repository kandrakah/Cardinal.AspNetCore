using System;
using System.Collections.Generic;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Extensão que faz a distinção de elementos de um enumerador baseando-se em uma condição estabelecida.
        /// </summary>        
        /// <typeparam name="TSource">Tipo do elemento enumerado</typeparam>
        /// <typeparam name="TKey">tipo do elemento projetado</typeparam>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="keySelector">Condicionamento de distinção à ser utilizado</param>
        /// <returns>O enumerador de elementos distintos.</returns>

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, null);
        }

        /// <summary>
        /// Extensão que faz a distinção de elementos de um enumerador baseando-se em uma condição estabelecida.
        /// </summary>        
        /// <typeparam name="TSource">Tipo do elemento enumerado</typeparam>
        /// <typeparam name="TKey">tipo do elemento projetado</typeparam>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="keySelector">Condicionamento de distinção à ser utilizado</param>
        /// <param name="comparer">Operação de comparação à ser aplicada</param>
        /// <returns>O enumerador de elementos distintos.</returns>

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return exec(); IEnumerable<TSource> exec()
            {
                var knownKeys = new HashSet<TKey>(comparer);
                foreach (var element in source)
                {
                    if (knownKeys.Add(keySelector(element)))
                        yield return element;
                }
            }
        }
    }
}
