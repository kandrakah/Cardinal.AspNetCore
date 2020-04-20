using System;

namespace Cardinal.AspNetCore.Exceptions
{
    /// <summary>
    /// Classe de exceção ligada à validação de versões.
    /// </summary>
    public class InvalidVersionException : CardinalException
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        public InvalidVersionException(string message) : base(message) { }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="message">Mensagem de exceção.</param>
        /// <param name="innerException">Exceção interior.</param>
        public InvalidVersionException(string message, Exception innerException) : base(message, innerException) { }

    }
}
