using System;
using System.Collections.Generic;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para IEnumerable.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Extensão que faz a distinção de de objetos de um enumerador com base em parâmetros fornecidos.
        /// </summary>
        /// <typeparam name="TSource">Tipo de objeto contido no enumerador.</typeparam>
        /// <typeparam name="TKey">Chave do enumerador.</typeparam>
        /// <param name="source">Este objeto.</param>
        /// <param name="keySelector">Parâmetros à serem usados na distinção.</param>
        /// <returns>Enumerador contendo objetos distinguidos com base nos parâmetros informados.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
