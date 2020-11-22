namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="object"/>
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Extensão que verifica se objeto é nulo.
        /// </summary>
        /// <param name="obj">Objeto referenciado.</param>
        /// <returns>Verdadeiro caso o objeto seja nulo e falso caso contrário.</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Extensão que verifica se objeto não é nulo.
        /// </summary>
        /// <param name="obj">Objeto referenciado.</param>
        /// <returns>Verdadeiro caso o objeto não seja nulo e falso caso contrário.</returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// Extensão que verifica se objeto é
        /// uma representação <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Typo do objeto de comparação.</typeparam>
        /// <param name="obj">Objeto referenciado.</param>
        /// <returns>Verdadeiro caso o objeto seja uma representação de <typeparamref name="T"/> e falso caso contrário.</returns>
        public static bool Is<T>(this object obj)
        {
            return obj?.GetType() == typeof(T);
        }

        /// <summary>
        /// Extensão que verifica se objeto não é
        /// uma representação <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Typo do objeto de comparação.</typeparam>
        /// <param name="obj">Objeto referenciado.</param>
        /// <returns>Verdadeiro caso o objeto não seja uma representação de <typeparamref name="T"/> e falso caso contrário.</returns>
        public static bool IsNot<T>(this object obj)
        {
            return obj?.GetType() != typeof(T);
        }

        /// <summary>
        /// Extensão que faz a conversão de um objeto
        /// em uma referência de <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo da classe em que o objeto será convertido.</typeparam>
        /// <param name="obj">Objeto referenciado.</param>
        /// <returns>Instância de <typeparamref name="T"/> referente ao objeto.</returns>
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }
    }
}
